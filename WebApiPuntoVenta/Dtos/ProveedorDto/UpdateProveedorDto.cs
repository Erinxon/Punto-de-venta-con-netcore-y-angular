using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPuntoVenta.Dtos.CategoriaDto;
using WebApiPuntoVenta.Dtos.DireccionDto;

namespace WebApiPuntoVenta.Dtos.ProveedorDto
{
    public class UpdateProveedorDto
    {
        public int Id { get; set; }
        public string Rnc { get; set; }
        public string RazonSocial { get; set; }
        public string Telefono { get; set; }
        public UpdateDireccion Direccion { get; set; }
        public DateTime? FechaCreado { get; set; }
        public bool? Estatus { get; set; }
    }
}