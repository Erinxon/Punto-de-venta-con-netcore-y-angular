using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiPuntoVenta.Dtos.VentasDto;
using WebApiPuntoVenta.Models;
using WebApiPuntoVenta.Response;
using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApiPuntoVenta.Dtos.UsuarioDto;
using WebApiPuntoVenta.Services.UsuarioService;
using WebApiPuntoVenta.Services.ServicesCliente;
using WebApiPuntoVenta.Dtos.ClienteDto;

namespace WebApiPuntoVenta.Services.VentasServices
{
    public class VentasServices : IVentasServices
    {
        private readonly PuntoDeVentaContext _Context;
        private readonly IMapper _mapper;
        private readonly IUsuarioServices _usuarioServices;
        private readonly IClienteServices _clienteServices;
        public VentasServices(PuntoDeVentaContext context, IMapper mapper,
            IUsuarioServices usuarioServices, IClienteServices clienteServices)
        {
            _mapper = mapper;
            _Context = context;
            _usuarioServices = usuarioServices;
            _clienteServices = clienteServices;
        }
        public async Task<ServiceResponse<GetVentasDto>> AddVenta(AddVentasDto newVenta)
        {
            var serviceResponse = new ServiceResponse<GetVentasDto>();
            try
            {
                Venta venta = _mapper.Map<Venta>(newVenta);
                venta.IdUsuario = await this._usuarioServices.GetUsuarioByEmail(newVenta.EmailUsuario);
                venta.Fecha = DateTime.Now;
                _Context.Ventas.Add(venta);
                await _Context.SaveChangesAsync();

                foreach (var item in newVenta.DetalleVentas)
                {
                    DetalleVenta detalle = _mapper.Map<DetalleVenta>(item);
                    //Update Stock producto
                    updateStockProducto(detalle);
                    //End Update Stock producto
                    detalle.IdVenta = venta.Id;
                    _Context.DetalleVentas.Add(detalle);
                }
                await _Context.SaveChangesAsync();
                serviceResponse.Data = await GetVentaById(venta.Id);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetVentasDto>>> GetAllVentas()
        {
            var serviceResponse = new ServiceResponse<List<GetVentasDto>>();
            List<GetDetalleVentasDto> list = new List<GetDetalleVentasDto>();
            try
            {
                List<GetVentasDto> getVentasDtos = await _Context.Ventas.Select(v =>
                _mapper.Map<GetVentasDto>(v)).ToListAsync();
                foreach (var item in getVentasDtos)
                {
                    item.Usuario = _Context.Usuarios.Where(x => x.Id == item.IdUsuario)
                        .Select(u => _mapper.Map<GetUsuarioDto>(u)).FirstOrDefault();

                    item.Cliente = _Context.Clientes.Where(x => x.Id == item.IdCliente)
                        .Select(c => _mapper.Map<GetClienteDto>(c)).FirstOrDefault();

                    item.DetalleVentas = await _Context.DetalleVentas
                    .Where(x => x.IdVenta == item.Id)
                    .Select(s => _mapper.Map<GetDetalleVentasDto>(s))
                    .ToListAsync();
                }
                serviceResponse.Data = getVentasDtos;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<GetVentasDto> GetVentaById(long id)
        {
            GetVentasDto getVentasDtos;
            List<GetDetalleVentasDto> list = new List<GetDetalleVentasDto>();

            getVentasDtos = await _Context.Ventas
                .Where(x => x.Id == id)
                .Select(v =>
                _mapper.Map<GetVentasDto>(v)).FirstOrDefaultAsync();

            getVentasDtos.Usuario = _Context.Usuarios.Where(x => x.Id == getVentasDtos.IdUsuario)
                        .Select(u => _mapper.Map<GetUsuarioDto>(u)).FirstOrDefault();

            getVentasDtos.Cliente = _Context.Clientes.Where(x => x.Id == getVentasDtos.IdCliente)
                .Select(c => _mapper.Map<GetClienteDto>(c)).FirstOrDefault();

            getVentasDtos.DetalleVentas = await _Context.DetalleVentas
                .Where(x => x.IdVenta == getVentasDtos.Id)
                .Select(s => _mapper.Map<GetDetalleVentasDto>(s))
                .ToListAsync();
            return getVentasDtos;
        }

        private void updateStockProducto(DetalleVenta detalleVenta)
        {
            var producto = _Context.Productos.Where(x => x.Id == detalleVenta.IdProducto).FirstOrDefault();
            producto.Stock -= detalleVenta.Cantidad;
            _Context.Attach(producto).State = EntityState.Modified;
            _Context.SaveChanges();
        }
    }
}