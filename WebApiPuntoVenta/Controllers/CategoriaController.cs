using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPuntoVenta.Models;
using WebApiPuntoVenta.Response;
using WebApiPuntoVenta.Services.ProductoService;
using WebApiPuntoVenta.Dtos.CategoriaDto;
using Microsoft.AspNetCore.Authorization;

namespace WebApiPuntoVenta.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetCategoriaDto>>>> Get()
        {
            var response = await _categoriaService.GetAllCategoria();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCategoriaDto>>> GetById(int id)
        {
            var response = await _categoriaService.GetCategoriaById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCategoriaDto>>> InsertCategoria([FromBody] AddCategoriaDto categoria)
        {
            var response = await _categoriaService.AddCategoria(categoria);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCategoriaDto>>> UpdateCategoria([FromBody] UpdateCategoriaDto categoria)
        {
            var response = await _categoriaService.Update(categoria);
            return Ok(response);
        }

        [HttpGet]
        [Route("filter/{busqueda}")]
        public async Task<ActionResult<ServiceResponse<GetCategoriaDto>>> GetFiilterCategoria(string busqueda)
        {
            var response = await _categoriaService.GetFiilterCategoria(busqueda);
            return Ok(response);
        }

        [HttpPut]
        [Route("updateEstatus/{id}")]
        public async Task<ActionResult<ServiceResponse<GetCategoriaDto>>> UpdateEstatusCategoria(long id)
        {
            var response = await _categoriaService.UpdateEstatusCategoria(id);
            return Ok(response);
        }
    }
}
