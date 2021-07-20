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

namespace WebApiPuntoVenta.Services.ProveedorService
{
    public interface IProveedorServices
    {
        Task<ServiceResponse<List<GetProveedorDto>>> GetAllProveedor();
        Task<ServiceResponse<List<GetProveedorDto>>> GetFilterProveedor(string search);
        Task<ServiceResponse<GetProveedorDto>> GetProveedorById(long id);
        Task<ServiceResponse<GetProveedorDto>> AddProveedor(AddProveedorDto newProveedor);
        Task<ServiceResponse<GetProveedorDto>> UpdateProveedor(UpdateProveedorDto updateProveedor);
        Task<ServiceResponse<GetProveedorDto>> UpdateEstatus(long id);
    }
}