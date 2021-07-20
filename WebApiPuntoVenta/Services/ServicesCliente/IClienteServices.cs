using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPuntoVenta.Dtos.ClienteDto;
using WebApiPuntoVenta.Models;
using WebApiPuntoVenta.Response;

namespace WebApiPuntoVenta.Services.ServicesCliente
{
    public interface IClienteServices
    {
        Task<ServiceResponse<List<GetClienteDto>>> GetAllCliente();
        Task<ServiceResponse<GetClienteDto>> GetClienteById(long id);
        Task<ServiceResponse<List<GetClienteDto>>> GetClienteFilter(string filters);
        Task<ServiceResponse<GetClienteDto>> UpdateEstatus(long id);
        Task<ServiceResponse<GetClienteDto>> AddCliente(AddClienteDto newCliente);
        Task<ServiceResponse<GetClienteDto>> UpdateCliente(UpdateClienteDto updateCliente);
    }
}