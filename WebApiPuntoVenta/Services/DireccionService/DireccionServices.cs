using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPuntoVenta.Models;

namespace WebApiPuntoVenta.Services.DireccionService
{
    public class DireccionServices : IDireccionServices
    {
        private readonly PuntoDeVentaContext _context;

        public DireccionServices(PuntoDeVentaContext context)
        {
            _context = context;
        }

        public async Task AddDireccion(Direccion direccion)
        {
            _context.Direccions.Add(direccion);
            await _context.SaveChangesAsync();
        }

        public async Task EditDireccion(Direccion direccion)
        {
            _context.Attach(direccion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Direccion>> GetAllDireccion()
        {
            return await _context.Direccions.ToListAsync();
        }

        public async Task<Direccion> GetDireccionById(int id)
        {
            return await _context.Direccions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public int GetLastIdDireccion()
        {
            var direccion = _context.Direccions
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();
            return direccion != null ? direccion.Id : 0;
        }
    }
}
