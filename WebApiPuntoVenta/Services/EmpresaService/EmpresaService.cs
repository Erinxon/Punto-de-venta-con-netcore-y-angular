using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPuntoVenta.Dtos.EmpresaDto;
using WebApiPuntoVenta.Models;
using WebApiPuntoVenta.Response;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace WebApiPuntoVenta.Services.EmpresaService
{
    public class EmpresaService : IEmpresaService
    {
        private readonly PuntoDeVentaContext _context;
        private readonly IMapper _mapper;

        public EmpresaService(PuntoDeVentaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetEmpresaDto>> AddEmpresa(AddEmpresaDto empresa)
        {

            var serviceResponse = new ServiceResponse<GetEmpresaDto>();
            try
            {
                var addEmpresa = _mapper.Map<Empresa>(empresa);
                addEmpresa.FechaCreado = DateTime.Now;
                addEmpresa.Estatus = true;
                _context.Empresas.Add(addEmpresa);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetEmpresaDto>(addEmpresa);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetEmpresaDto>>> GetEmpresa()
        {

            var serviceResponse = new ServiceResponse<List<GetEmpresaDto>>();
            try
            {
                serviceResponse.Data = await _context.Empresas
                    .Select(e => _mapper.Map<GetEmpresaDto>(e))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmpresaDto>> UpdateEmpresa(UpdateEmpresaDto empresa)
        {
            var serviceResponse = new ServiceResponse<GetEmpresaDto>();
            try
            {
                var updateEmpresa = _mapper.Map<Empresa>(empresa);
                _context.Attach(updateEmpresa).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetEmpresaDto>(updateEmpresa);
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
