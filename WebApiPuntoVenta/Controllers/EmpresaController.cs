using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPuntoVenta.Dtos.EmpresaDto;
using WebApiPuntoVenta.Response;
using WebApiPuntoVenta.Services.EmpresaService;

namespace WebApiPuntoVenta.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class EmpresaController : Controller
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetEmpresaDto>>>> Get()
        {
            var response = await _empresaService.GetEmpresa();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetEmpresaDto>>> AddEmpresa([FromBody] AddEmpresaDto empresa)
        {
            var response = await _empresaService.AddEmpresa(empresa);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetEmpresaDto>>> UpdateEmpresa([FromBody] UpdateEmpresaDto empresa)
        {
            var response = await _empresaService.UpdateEmpresa(empresa);
            return Ok(response);
        }
    }
}
