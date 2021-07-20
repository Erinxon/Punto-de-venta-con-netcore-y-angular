using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPuntoVenta.Models;

namespace WebApiPuntoVenta.Services.DireccionService
{
    public interface IDireccionServices
    {
        Task<List<Direccion>> GetAllDireccion();
        Task<Direccion> GetDireccionById(int id);
        Task AddDireccion(Direccion direccion);
        Task EditDireccion(Direccion direccion);
        int GetLastIdDireccion();
    }
}
