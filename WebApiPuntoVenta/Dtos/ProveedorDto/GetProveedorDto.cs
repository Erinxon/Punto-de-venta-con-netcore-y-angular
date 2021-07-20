using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebApiPuntoVenta.Dtos.DireccionDto;
using WebApiPuntoVenta.Models; 

namespace WebApiPuntoVenta.Dtos.ProveedorDto
{
    public class GetProveedorDto
    {
        public int Id { get; set; }
        public string Rnc { get; set; }
        public string RazonSocial { get; set; }
        public string Telefono { get; set; }
        [JsonIgnore]
        public int? IdDireccion { get; set; }
        public GetDireccionDto  Direccion { get; set; }
        public DateTime? FechaCreado { get; set; }
        public bool? Estatus { get; set; }
    }
}