using System;
using System.Text.Json.Serialization;
using WebApiPuntoVenta.Dtos.DireccionDto;
using WebApiPuntoVenta.Models;

namespace WebApiPuntoVenta.Dtos.ClienteDto
{
    public class GetClienteDto
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }
        [JsonIgnore]
        public int? IdDireccion { get; set; }
        public GetDireccionDto Direccion { get; set; }
        public DateTime? FechaCreado { get; set; }
        public bool? Estatus { get; set; }
    }
}