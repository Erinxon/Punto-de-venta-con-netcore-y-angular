using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPuntoVenta.Dtos.ComprasDto;
using WebApiPuntoVenta.Models;
using WebApiPuntoVenta.Response;
using WebApiPuntoVenta.Services.CompraService;


namespace WebApiPuntoVenta.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class CompraController : ControllerBase
    {
        private readonly ICompraServices _CompraServices;
        public CompraController(ICompraServices compraServices)
        {
            _CompraServices = compraServices;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetCompraDto>>>> Get()
        {
            var response = await _CompraServices.GetAllCompra();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCompraDto>>> GetById(long id)
        {
            var response = new ServiceResponse<GetCompraDto>();
            try{
                response.Data = await _CompraServices.GetCompraById(id);
            }catch(Exception ex){
                response.Success = false;
                response.Message = ex.Message;
            }      
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCompraDto>>> InsertCompra([FromBody]AddCompraDto newCompra)
        {
            var response = await _CompraServices.AddCompra(newCompra);
            return Ok(response);
        }
    }
}