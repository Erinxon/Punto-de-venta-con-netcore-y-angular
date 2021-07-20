using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiPuntoVenta.Dtos.ComprasDto;
using WebApiPuntoVenta.Models;
using WebApiPuntoVenta.Response;
using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApiPuntoVenta.Services.UsuarioService;
using WebApiPuntoVenta.Services.ProductoService;
using WebApiPuntoVenta.Services.ProveedorService;
using WebApiPuntoVenta.Dtos.UsuarioDto;
using WebApiPuntoVenta.Dtos.ProveedorDto;

namespace WebApiPuntoVenta.Services.CompraService
{
    public class CompraServices : ICompraServices
    {
        private readonly PuntoDeVentaContext _Context;
        private readonly IMapper _mapper;
        private readonly IUsuarioServices _usuarioServices;

        public CompraServices(PuntoDeVentaContext context, IMapper mapper, IUsuarioServices usuarioServices)
        {
            _Context = context;
            _mapper = mapper;
            _usuarioServices = usuarioServices;

        }
        public async Task<ServiceResponse<GetCompraDto>> AddCompra(AddCompraDto newCmpra)
        {
            var serviceResponse = new ServiceResponse<GetCompraDto>();
            try
            {
                Compra compra = _mapper.Map<Compra>(newCmpra);
                compra.IdUsuario = await this._usuarioServices.GetUsuarioByEmail(newCmpra.EmailUsuario);
                compra.Fecha = DateTime.Now;
                _Context.Compras.Add(compra);
                await _Context.SaveChangesAsync();

                foreach (var item in newCmpra.DetalleCompra)
                {
                    DetalleCompra detalle = _mapper.Map<DetalleCompra>(item);
                    detalle.IdCompra = compra.Id;
                    //Actualizando stock del producto
                    updateStockProducto(detalle);
                    //Producto actualizado
                    _Context.DetalleCompras.Add(detalle);
                }
                await _Context.SaveChangesAsync();
                serviceResponse.Data = await GetCompraById(compra.Id);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCompraDto>>> GetAllCompra()
        {
            var serviceResponse = new ServiceResponse<List<GetCompraDto>>();
            List<GetDetalleCompraDto> list = new List<GetDetalleCompraDto>();
            try
            {
                List<GetCompraDto> getCompraDtos = await _Context.Compras.Select(c =>
                _mapper.Map<GetCompraDto>(c)).ToListAsync();
                foreach (var item in getCompraDtos)
                {
                    item.Usuario = _Context.Usuarios.Where(x => x.Id == item.IdUsuario)
                      .Select(u => _mapper.Map<GetUsuarioDto>(u)).FirstOrDefault();

                    item.Proveedor = _Context.Proveedores.Where(x => x.Id == item.IdProveedor)
                      .Select(p => _mapper.Map<GetProveedorDto>(p)).FirstOrDefault();

                    item.DetalleCompra = await _Context.DetalleCompras
                    .Where(x => x.IdCompra == item.Id)
                    .Select(s => _mapper.Map<GetDetalleCompraDto>(s))
                    .ToListAsync();
                }
                serviceResponse.Data = getCompraDtos;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false; 
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<GetCompraDto> GetCompraById(long id)
        {
            GetCompraDto getCompraDtos;
            List<GetDetalleCompraDto> list = new List<GetDetalleCompraDto>();

            getCompraDtos = await _Context.Compras
                .Where(x => x.Id == id)
                .Select(c =>
                _mapper.Map<GetCompraDto>(c)).FirstOrDefaultAsync();

            getCompraDtos.Usuario = _Context.Usuarios.Where(x => x.Id == getCompraDtos.IdUsuario)
                       .Select(u => _mapper.Map<GetUsuarioDto>(u)).FirstOrDefault();

            getCompraDtos.Proveedor = _Context.Proveedores.Where(x => x.Id == getCompraDtos.IdProveedor)
                      .Select(u => _mapper.Map<GetProveedorDto>(u)).FirstOrDefault();

            getCompraDtos.DetalleCompra = await _Context.DetalleCompras
                .Where(x => x.IdCompra == getCompraDtos.Id)
                .Select(s => _mapper.Map<GetDetalleCompraDto>(s))
                .ToListAsync();
            return getCompraDtos;
        }

        private void updateStockProducto(DetalleCompra detalleCompra)
        {
            var producto = _Context.Productos.Where(x => x.Id == detalleCompra.IdProducto).FirstOrDefault();
            producto.Stock += detalleCompra.Cantidad;
            producto.Costo = detalleCompra.Costo;
            _Context.Attach(producto).State = EntityState.Modified;
            _Context.SaveChanges();
        }
    }
}