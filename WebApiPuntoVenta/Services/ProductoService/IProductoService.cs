using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPuntoVenta.Dtos.ProductDto;
using WebApiPuntoVenta.Models;
using WebApiPuntoVenta.Response;

namespace WebApiPuntoVenta.Services.ProductoService
{
    public interface IProductoService
    {
        Task<ServiceResponse<List<GetProductosDto>>> GetAllProductos();
        Task<ServiceResponse<List<GetProductosDto>>> GetFilterProductos(string search);
        Task<ServiceResponse<GetProductosDto>> GetProductoById(long id);
        Task<ServiceResponse<GetProductosDto>> AddProducto(AddProductoDto producto);
        Task<ServiceResponse<GetProductosDto>> Update(UpdateProductoDto producto);
        Task<ServiceResponse<GetProductosDto>> UpdateEstatusProducto(long id);
    }
}
