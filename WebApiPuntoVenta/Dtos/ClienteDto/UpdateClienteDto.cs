using System;
using WebApiPuntoVenta.Dtos.DireccionDto;

namespace WebApiPuntoVenta.Dtos.ClienteDto
{
    public class UpdateClienteDto
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }
        public UpdateDireccion Direccion { get; set; }
        public DateTime? FechaCreado { get; set; }
        public bool? Estatus { get; set; }
    }
}