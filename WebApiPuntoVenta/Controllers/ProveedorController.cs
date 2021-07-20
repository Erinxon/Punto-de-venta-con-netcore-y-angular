using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPuntoVenta.Dtos.ProveedorDto;
using WebApiPuntoVenta.Models;
using WebApiPuntoVenta.Response;
using WebApiPuntoVenta.Services.ProveedorService;

namespace WebApiPuntoVenta.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorServices _proveedorServices;
        public ProveedorController(IProveedorServices proveedorServices)
        {
            _proveedorServices = proveedorServices;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetProveedorDto>>>> Get()
        {
            var response = await _proveedorServices.GetAllProveedor();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetProveedorDto>>> GetByID(int id)
        {
            var response = await _proveedorServices.GetProveedorById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetProveedorDto>>> InsertProveedor([FromBody] AddProveedorDto newProveedor)
        {
            var response = await _proveedorServices.AddProveedor(newProveedor);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetProveedorDto>>> UpdateProveedor([FromBody] UpdateProveedorDto updateProveedor)
        {
            var response = await _proveedorServices.UpdateProveedor(updateProveedor);
            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateEstatus/{id}")]
        public async Task<ActionResult<ServiceResponse<GetProveedorDto>>> UpdateEstatus(long id)
        {
            var response = await _proveedorServices.UpdateEstatus(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("filterProveedor/{search}")]
        public async Task<ActionResult<ServiceResponse<List<GetProveedorDto>>>> GetFilterProveedor(string search)
        {
            var response = await _proveedorServices.GetFilterProveedor(search);
            return Ok(response);
        }



    }
}