using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiPuntoVenta.Dtos.ProveedorDto;
using WebApiPuntoVenta.Dtos.DireccionDto;
using WebApiPuntoVenta.Models;
using WebApiPuntoVenta.Response;
using WebApiPuntoVenta.Services.DireccionService;

namespace WebApiPuntoVenta.Services.ProveedorService
{
    public class ProveedorServices : IProveedorServices
    {
        private readonly PuntoDeVentaContext _context;
        private readonly IMapper _mapper;
        private readonly IDireccionServices _direccionServices;
  
        public ProveedorServices(PuntoDeVentaContext context, 
        IMapper mapper, IDireccionServices direccionServices)
        {          
            _context = context;
            _mapper = mapper;
            _direccionServices = direccionServices;
        }

        public async Task<ServiceResponse<GetProveedorDto>> AddProveedor(AddProveedorDto newProveedor)
        {
            var serviceResponse = new ServiceResponse<GetProveedorDto>();
            try
            {
                Direccion direccion = new Direccion
                {
                    Provincia = newProveedor.Direccion.Provincia,
                    Ciudad = newProveedor.Direccion.Ciudad,
                    Calle = newProveedor.Direccion.Calle,
                    NumeroCasa = newProveedor.Direccion.NumeroCasa
                };
                await _direccionServices.AddDireccion(direccion);
                
                Proveedore proveedor = _mapper.Map<Proveedore>(newProveedor);
                proveedor.FechaCreado = DateTime.Now.Date;
                proveedor.Estatus = true;
                proveedor.IdDireccion = _direccionServices.GetLastIdDireccion();
                _context.Proveedores.Add(proveedor);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetProveedorDto>(proveedor);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.InnerException.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProveedorDto>>> GetAllProveedor()
        {
            var serviceResponse = new ServiceResponse<List<GetProveedorDto>>();
            try{
                serviceResponse.Data = await _context.Proveedores.
                    Select(c => _mapper.Map<GetProveedorDto>(c)).ToListAsync();

                serviceResponse.Data.ForEach(c =>
                {
                    c.Direccion = _context.Direccions.Where(x => x.Id == c.IdDireccion)
                                  .Select(x => _mapper.Map<GetDireccionDto>(x))
                                  .FirstOrDefault();
                });
            }
            catch(Exception ex){
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProveedorDto>>> GetFilterProveedor(string search)
        {
            var serviceResponse = new ServiceResponse<List<GetProveedorDto>>();
            try
            {
                var proveedores = await _context.Proveedores.ToArrayAsync();

                serviceResponse.Data = proveedores.Where(x => x.Rnc.Contains(search)
                   || x.RazonSocial.Contains(search))
                    .Select(c => _mapper.Map<GetProveedorDto>(c)).ToList(); ;

                serviceResponse.Data.ForEach(c =>
                {
                    c.Direccion = _context.Direccions.Where(x => x.Id == c.IdDireccion)
                                  .Select(x => _mapper.Map<GetDireccionDto>(x))
                                  .FirstOrDefault();
                });
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProveedorDto>> GetProveedorById(long id)
        {
            var serviceResponse = new ServiceResponse<GetProveedorDto>();
            try{
               Proveedore proveedor = await _context.Proveedores.FirstOrDefaultAsync(c => c.Id == id);
               serviceResponse.Data = _mapper.Map<GetProveedorDto>(proveedor);
               serviceResponse.Data.Direccion = await _context.Direccions.Where(x => x.Id == proveedor.IdDireccion)
                                                 .Select(p => _mapper.Map<GetDireccionDto>(p)).FirstOrDefaultAsync();                                            
            }
            catch(Exception ex){
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProveedorDto>> UpdateEstatus(long id)
        {
            var serviceResponse = new ServiceResponse<GetProveedorDto>();
            try
            {
                Proveedore proveedor = await _context.Proveedores.FirstOrDefaultAsync(c => c.Id == id);
                if(proveedor != null)
                {
                    proveedor.Estatus = !proveedor.Estatus;
                    _context.Proveedores.Attach(proveedor).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetProveedorDto>(proveedor);
                    serviceResponse.Data.Direccion = await _context.Direccions.Where(x => x.Id == proveedor.IdDireccion)
                                                .Select(p => _mapper.Map<GetDireccionDto>(p)).FirstOrDefaultAsync();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Proveedor no encontrado";
                }
               
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProveedorDto>> UpdateProveedor(UpdateProveedorDto updateProveedor)
        {
            var serviceResponse = new ServiceResponse<GetProveedorDto>();
            try
            {
                Direccion direccion = new Direccion
                {
                    Id = (int) updateProveedor.Direccion.Id,
                    Provincia = updateProveedor.Direccion.Provincia,
                    Ciudad = updateProveedor.Direccion.Ciudad,
                    Calle = updateProveedor.Direccion.Calle,
                    NumeroCasa = updateProveedor.Direccion.NumeroCasa,
                };       
                await _direccionServices.EditDireccion(direccion);

                Proveedore proveedor = _mapper.Map<Proveedore>(updateProveedor);
                proveedor.IdDireccion = updateProveedor.Direccion.Id;
                _context.Attach(proveedor).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetProveedorDto>(proveedor);
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