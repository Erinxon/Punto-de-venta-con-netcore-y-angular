using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiPuntoVenta.Dtos.ComprasDto;
using WebApiPuntoVenta.Response;

namespace WebApiPuntoVenta.Services.CompraService
{
    public interface ICompraServices
    {
        Task<ServiceResponse<List<GetCompraDto>>> GetAllCompra();
        Task<GetCompraDto> GetCompraById(long id);
        Task<ServiceResponse<GetCompraDto>> AddCompra(AddCompraDto newCmpra);
    }
}