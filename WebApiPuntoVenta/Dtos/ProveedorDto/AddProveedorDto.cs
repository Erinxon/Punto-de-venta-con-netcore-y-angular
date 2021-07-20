using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPuntoVenta.Dtos.DireccionDto;

namespace WebApiPuntoVenta.Dtos.ProveedorDto
{
    public class AddProveedorDto
    {
        public string Rnc { get; set; }
        public string RazonSocial { get; set; }
        public string Telefono { get; set; }
        public AddDireccionDto Direccion { get; set; }
    }
}