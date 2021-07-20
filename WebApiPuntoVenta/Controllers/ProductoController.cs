using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPuntoVenta.Dtos.ProductDto;
using WebApiPuntoVenta.Models;
using WebApiPuntoVenta.Response;
using WebApiPuntoVenta.Services.ProductoService;

namespace WebApiPuntoVenta.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetProductosDto>>>> Get()
        {
            var response = await _productoService.GetAllProductos();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetProductosDto>>> GetByID(int id)
        {
            var response = await _productoService.GetProductoById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetProductosDto>>> InsertProducto(AddProductoDto producto)
        {
            var response = await _productoService.AddProducto(producto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetProductosDto>>> UpdateProducto(UpdateProductoDto producto)
        {
            var response = await _productoService.Update(producto);
            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateEstatusProducto/{id}")]
        public async Task<ActionResult<ServiceResponse<GetProductosDto>>> UpdateEstatusProducto(long id)
        {
            var response = await _productoService.UpdateEstatusProducto(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("filter/{search}")]
        public async Task<ActionResult<ServiceResponse<List<GetProductosDto>>>> GetFilterProductos(string search)
        {
            var response = await _productoService.GetFilterProductos(search);
            return Ok(response);
        }



    }
}
