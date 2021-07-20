using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPuntoVenta.Dtos.VentasDto;
using WebApiPuntoVenta.Models;
using WebApiPuntoVenta.Response;
using WebApiPuntoVenta.Services.VentasServices;

namespace WebApiPuntoVenta.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class VentaController : ControllerBase
    {
        private readonly IVentasServices _VentasServices;
        public VentaController(IVentasServices ventasServices)
        {
            _VentasServices = ventasServices;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetVentasDto>>>> Get()
        {
            var response = await _VentasServices.GetAllVentas();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetVentasDto>>> GetById(long id)
        {
            var response = new ServiceResponse<GetVentasDto>();
            try{
                response.Data = await _VentasServices.GetVentaById(id);
            }catch(Exception ex){
                response.Success = false;
                response.Message = ex.Message;
            }      
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetVentasDto>>> InsertVentas([FromBody]AddVentasDto newVenta)
        {
            var response = await _VentasServices.AddVenta(newVenta);
            return Ok(response);
        }
    }
}