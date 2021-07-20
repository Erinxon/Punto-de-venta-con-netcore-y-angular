using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPuntoVenta.Dtos.EmpresaDto;
using WebApiPuntoVenta.Response;

namespace WebApiPuntoVenta.Services.EmpresaService
{
    public interface IEmpresaService
    {
        Task<ServiceResponse<List<GetEmpresaDto>>> GetEmpresa();
        Task<ServiceResponse<GetEmpresaDto>> AddEmpresa(AddEmpresaDto empresa);
        Task<ServiceResponse<GetEmpresaDto>> UpdateEmpresa(UpdateEmpresaDto empresa);
    }
}
