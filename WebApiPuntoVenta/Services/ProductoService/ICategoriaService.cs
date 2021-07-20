using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPuntoVenta.Models;
using WebApiPuntoVenta.Response;
using WebApiPuntoVenta.Dtos.CategoriaDto;

namespace WebApiPuntoVenta.Services.ProductoService
{
    public interface ICategoriaService
    {
        Task<ServiceResponse<List<GetCategoriaDto>>> GetAllCategoria();
        Task<ServiceResponse<List<GetCategoriaDto>>> GetFiilterCategoria(string filter);
        Task<ServiceResponse<GetCategoriaDto>> GetCategoriaById(long id);
        Task<ServiceResponse<GetCategoriaDto>> AddCategoria(AddCategoriaDto categoria);
        Task<ServiceResponse<GetCategoriaDto>> Update(UpdateCategoriaDto categoria);
        Task<ServiceResponse<GetCategoriaDto>> UpdateEstatusCategoria(long id);
    }
}
