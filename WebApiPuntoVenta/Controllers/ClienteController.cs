using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPuntoVenta.Models;
using WebApiPuntoVenta.Response;
using WebApiPuntoVenta.Services.ServicesCliente;
using WebApiPuntoVenta.Dtos.ClienteDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace WebApiPuntoVenta.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteServices _clienteServices;
        public ClienteController(IClienteServices clienteServices)
        {
            _clienteServices = clienteServices;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetClienteDto>>>> Get()
        {
            var response = await _clienteServices.GetAllCliente();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetClienteDto>>> GetClienteById(int id)
        {
            var response = await _clienteServices.GetClienteById(id);
            return Ok(response);

        }

        [HttpGet]
        [Route("filtro/{filter}")]
        public async Task<ActionResult<ServiceResponse<List<GetClienteDto>>>> GetClienteFilter(string filter)
        {
            var response = await _clienteServices.GetClienteFilter(filter);
            return Ok(response);

        }

        [HttpPut]
        [Route("updateEstatus/{id}")]
        public async Task<ActionResult<ServiceResponse<GetClienteDto>>> UpdateEstatud(long id)
        {
            var response = await _clienteServices.UpdateEstatus(id);
            return Ok(response);

        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetClienteDto>>> InsertCliente([FromBody] AddClienteDto newCliente)
        {
            var response = await _clienteServices.AddCliente(newCliente);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetClienteDto>>> UpdateCliente([FromBody] UpdateClienteDto cliente)
        {
            var response = await _clienteServices.UpdateCliente(cliente);
            return Ok(response);
        }

    }
}