using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPuntoVenta.Dtos.UsuarioDto;
using WebApiPuntoVenta.Models;
using WebApiPuntoVenta.Response;
using WebApiPuntoVenta.Services.UsuarioService;

namespace WebApiPuntoVenta.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServices _usuarioServices;
        public UsuarioController(IUsuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }


        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<UserResponse>>> Autenticar([FromBody] AuthUsuarioDto authUsuario){
            var response = await _usuarioServices.Autenticar(authUsuario);
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<List<GetUsuarioDto>>>> Get()
        {
            var response = await _usuarioServices.GetAllUsuario();
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<GetUsuarioDto>>> GetById(long id)
        {
            var response = await _usuarioServices.GetUsuarioById(id);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<GetUsuarioDto>>> InsertUsuario([FromBody]AddUsuarioDto newUsuario)
        {
            var response = await _usuarioServices.AddUsuario(newUsuario);
            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<GetUsuarioDto>>> UpdateUsuario([FromBody]UpdateUsuarioDto updateUsuario)
        {
            var response = await _usuarioServices.Update(updateUsuario);
            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateEstatusUsuario/{id}")]
        public async Task<ActionResult<ServiceResponse<GetUsuarioDto>>> UpdateEstatusUsuario(long id)
        {
            var response = await _usuarioServices.UpdateEstatusUsuario(id);
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        [Route("FilterUsuario/{search}")]
        public async Task<ActionResult<ServiceResponse<List<GetUsuarioDto>>>> FilterUsuario(string search)
        {
            var response = await _usuarioServices.FilterUsuario(search);
            return Ok(response);
        }
       
    }
}