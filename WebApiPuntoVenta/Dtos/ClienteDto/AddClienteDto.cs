using System;
using WebApiPuntoVenta.Models;
using WebApiPuntoVenta.Dtos.DireccionDto;

namespace WebApiPuntoVenta.Dtos.ClienteDto
{
    public class AddClienteDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }
        public AddDireccionDto Direccion {get;set;} 

    }
}