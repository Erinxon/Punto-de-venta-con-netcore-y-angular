using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiPuntoVenta.Dtos.CategoriaDto;
using WebApiPuntoVenta.Models;
using WebApiPuntoVenta.Response;

namespace WebApiPuntoVenta.Services.ProductoService
{
    public class CategoriaService : ICategoriaService
    {
        
        private readonly PuntoDeVentaContext _context;
        private readonly IMapper _mapper;

        public CategoriaService(PuntoDeVentaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetCategoriaDto>> AddCategoria(AddCategoriaDto nuewCategoria)
        {
            var serviceResponse = new ServiceResponse<GetCategoriaDto>();
            try
            {
                Categoria categoria = _mapper.Map<Categoria>(nuewCategoria);
                categoria.FechaCreado = DateTime.Now;
                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCategoriaDto>(categoria);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
           
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCategoriaDto>>> GetAllCategoria()
        {
            var serviceResponse = new ServiceResponse<List<GetCategoriaDto>>();
            try
            {
                serviceResponse.Data = await _context.Categorias
                    .Select(c => _mapper.Map<GetCategoriaDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;


        }

        public async Task<ServiceResponse<GetCategoriaDto>> GetCategoriaById(long id)
        {
            var serviceResponse = new ServiceResponse<GetCategoriaDto>();
            try
            {
                Categoria categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
                serviceResponse.Data = _mapper.Map<GetCategoriaDto>(categoria);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCategoriaDto>>> GetFiilterCategoria(string filter)
        {
            var serviceResponse = new ServiceResponse<List<GetCategoriaDto>>();
            try
            {
                serviceResponse.Data = await _context.Categorias.Where(x=>x.Nombre.Contains(filter))
                    .Select(c => _mapper.Map<GetCategoriaDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCategoriaDto>> Update(UpdateCategoriaDto UpdateCategoria)
        {
            var serviceResponse = new ServiceResponse<GetCategoriaDto>();
            try
            {
                Categoria categoria = _mapper.Map<Categoria>(UpdateCategoria);
                _context.Attach(categoria).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCategoriaDto>(categoria);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
           
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCategoriaDto>> UpdateEstatusCategoria(long id)
        {
            var serviceResponse = new ServiceResponse<GetCategoriaDto>();
            try
            {

                Categoria categoria = await _context.Categorias.Where(x => x.Id == id).FirstOrDefaultAsync();
                categoria.Estatus = !categoria.Estatus;
                _context.Attach(categoria).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCategoriaDto>(categoria);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
