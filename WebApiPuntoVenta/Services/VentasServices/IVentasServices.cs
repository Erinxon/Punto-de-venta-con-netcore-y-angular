using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiPuntoVenta.Dtos.VentasDto;
using WebApiPuntoVenta.Response;


namespace WebApiPuntoVenta.Services.VentasServices
{
    public interface IVentasServices
    {
         Task<ServiceResponse<List<GetVentasDto>>> GetAllVentas();
         Task<GetVentasDto> GetVentaById(long id);
         Task<ServiceResponse<GetVentasDto>> AddVenta(AddVentasDto newVenta);
        
    }
}