using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiPuntoVenta.Dtos.CategoriaDto;
using WebApiPuntoVenta.Dtos.ProductDto;
using WebApiPuntoVenta.Models;
using WebApiPuntoVenta.Response;

namespace WebApiPuntoVenta.Services.ProductoService
{
    public class ProductoService : IProductoService
    {    
        private readonly PuntoDeVentaContext _context;
        private readonly IMapper _mapper;

        public ProductoService(PuntoDeVentaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetProductosDto>> AddProducto(AddProductoDto nuewProducto)
        {
            var serviceResponse = new ServiceResponse<GetProductosDto>();
            try
            {
                Producto producto = _mapper.Map<Producto>(nuewProducto);
                producto.FechaCreado = DateTime.Now;
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();

                GetProductosDto getProductosDto = _mapper.Map<GetProductosDto>(producto);
                getProductosDto.Categoria = _context.Categorias.Where(x => x.Id == producto.IdCategoria)
                                            .Select(x => _mapper.Map<GetCategoriaDto>(x)).FirstOrDefault();

                serviceResponse.Data = getProductosDto;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }      
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductosDto>>> GetAllProductos()
        {
            var serviceResponse = new ServiceResponse<List<GetProductosDto>>();
            try
            {
                serviceResponse.Data = await _context.Productos.
                    Select(p => _mapper.Map<GetProductosDto>(p)).ToListAsync();

                serviceResponse.Data.ForEach(p =>
                {
                    p.Categoria = _context.Categorias.Where(x => x.Id == p.IdCategoria)
                                    .Select(c => _mapper.Map<GetCategoriaDto>(c)).FirstOrDefault();
                });

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }     
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductosDto>>> GetFilterProductos(string search)
        {
            var serviceResponse = new ServiceResponse<List<GetProductosDto>>();
            try
            {
                var allProducto = await _context.Productos.ToListAsync();

                serviceResponse.Data = allProducto.Where(x => x.NombreProducto.ToLower().Contains(search.ToLower()))
                    .Select(p => _mapper.Map<GetProductosDto>(p)).ToList();

                serviceResponse.Data.ForEach(p =>
                {
                    p.Categoria = _context.Categorias.Where(x => x.Id == p.IdCategoria)
                                    .Select(c => _mapper.Map<GetCategoriaDto>(c)).FirstOrDefault();
                });

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductosDto>> GetProductoById(long id)
        {
            var serviceResponse = new ServiceResponse<GetProductosDto>();
            try
            {
                Producto producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);
                GetProductosDto getProductosDto = _mapper.Map<GetProductosDto>(producto);
                getProductosDto.Categoria = _context.Categorias.Where(x => x.Id == producto.IdCategoria)
                                            .Select(x => _mapper.Map<GetCategoriaDto>(x)).FirstOrDefault();
                serviceResponse.Data = getProductosDto;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }         
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductosDto>> Update(UpdateProductoDto updateProducto)
        {
            var serviceResponse = new ServiceResponse<GetProductosDto>();
            try
            {
                Producto producto = _mapper.Map<Producto>(updateProducto);
                _context.Entry(producto).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetProductosDto>(producto);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }              
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductosDto>> UpdateEstatusProducto(long id)
        {
            var serviceResponse = new ServiceResponse<GetProductosDto>();
            try
            {
                Producto producto = await _context.Productos.FirstOrDefaultAsync(x => x.Id == id);
                producto.Estatus = !producto.Estatus;
                _context.Attach(producto).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                serviceResponse = await GetProductoById(producto.Id);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
         
        }
    }
}
