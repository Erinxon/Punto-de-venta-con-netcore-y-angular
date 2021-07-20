using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiPuntoVenta.Dtos.ClienteDto;
using WebApiPuntoVenta.Response;
using WebApiPuntoVenta.Models;
using System;
using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApiPuntoVenta.Services.DireccionService;
using WebApiPuntoVenta.Dtos.DireccionDto;

namespace WebApiPuntoVenta.Services.ServicesCliente
{
    public class ClienteServices : IClienteServices
    {
        private readonly PuntoDeVentaContext _context;
        private readonly IMapper _mapper;
        private readonly IDireccionServices _direccionServices;
  
        public ClienteServices(PuntoDeVentaContext context, IMapper mapper, IDireccionServices direccionServices)
        {          
            _context = context;
            _mapper = mapper;
            _direccionServices = direccionServices;
        }

        public async Task<ServiceResponse<GetClienteDto>> AddCliente(AddClienteDto newCliente)
        {
            var serviceResponse = new ServiceResponse<GetClienteDto>();
            try
            {
                Direccion direccion = new Direccion
                {
                    Provincia = newCliente.Direccion.Provincia,
                    Ciudad = newCliente.Direccion.Ciudad,
                    Calle = newCliente.Direccion.Calle,
                    NumeroCasa = newCliente.Direccion.NumeroCasa
                };
                await _direccionServices.AddDireccion(direccion);
                
                Cliente cliente = _mapper.Map<Cliente>(newCliente);
                cliente.FechaCreado = DateTime.Now;
                cliente.Estatus = true;
                cliente.IdDireccion = _direccionServices.GetLastIdDireccion();
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetClienteDto>(cliente);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.InnerException.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetClienteDto>>> GetAllCliente()
        {
            var serviceResponse = new ServiceResponse<List<GetClienteDto>>();
            try{
                serviceResponse.Data = await _context.Clientes.
                    Select(c => _mapper.Map<GetClienteDto>(c)).ToListAsync();

                serviceResponse.Data.ForEach(c =>
                {
                    c.Direccion = _context.Direccions.Where(x => x.Id == c.IdDireccion)
                                  .Select(x=> _mapper.Map<GetDireccionDto>(x))
                                  .FirstOrDefault();
                });
            }
            catch(Exception ex){
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetClienteDto>> GetClienteById(long id)
        {
            var serviceResponse = new ServiceResponse<GetClienteDto>();
            try{
               Cliente cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
               if(cliente == null)
               {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Cliente no encontrado";
                }
                else
                {
                    GetClienteDto getClienteDto = _mapper.Map<GetClienteDto>(cliente);
                    getClienteDto.Direccion = await _context.Direccions.Where(x => x.Id == cliente.IdDireccion)
                          .Select(c => _mapper.Map<GetDireccionDto>(c)).FirstOrDefaultAsync();
                    serviceResponse.Data = getClienteDto;
                }          
            }
            catch(Exception ex){
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetClienteDto>> UpdateCliente(UpdateClienteDto updateCliente)
        {
            var serviceResponse = new ServiceResponse<GetClienteDto>();
            try
            {
                Direccion direccion = new Direccion
                {
                    Id = updateCliente.Direccion.Id,
                    Provincia = updateCliente.Direccion.Provincia,
                    Ciudad = updateCliente.Direccion.Ciudad,
                    Calle = updateCliente.Direccion.Calle,
                    NumeroCasa = updateCliente.Direccion.NumeroCasa
                };
                await _direccionServices.EditDireccion(direccion);

                Cliente cliente = _mapper.Map<Cliente>(updateCliente);
                cliente.IdDireccion = direccion.Id;
                _context.Attach(cliente).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetClienteDto>(cliente);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetClienteDto>>> GetClienteFilter(string filters)
        {
            var serviceResponse = new ServiceResponse<List<GetClienteDto>>();
            try
            {
                var allClientes = await _context.Clientes.ToListAsync();

                var clienteFilter = allClientes.Where(x => x.Nombre.Contains(filters)
                || x.Apellido.Contains(filters) || ($"{x.Nombre} {x.Nombre}").Contains(filters)
                || x.Cedula.Contains(filters) || x.Email.Contains(filters) || x.Telefono.Contains(filters))
                    .Select(c => _mapper.Map<GetClienteDto>(c)).ToList();

                serviceResponse.Data = clienteFilter;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetClienteDto>> UpdateEstatus(long id)
        {
            var serviceResponse = new ServiceResponse<GetClienteDto>();

            var cliente = await this._context.Clientes.FirstOrDefaultAsync(x => x.Id == id);
            cliente.Estatus = !cliente.Estatus;

            _context.Attach(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetClienteDto>(cliente);
            return serviceResponse;
        }
    }
}