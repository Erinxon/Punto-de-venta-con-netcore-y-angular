using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using WebApiPuntoVenta.Dtos.ClienteDto;
using WebApiPuntoVenta.Dtos.UsuarioDto;

namespace WebApiPuntoVenta.Dtos.VentasDto
{
    public class GetVentasDto
    {
        public long Id { get; set; }
        public DateTime? Fecha { get; set; }
        [JsonIgnore]
        public int? IdUsuario { get; set; }
        public GetUsuarioDto Usuario { get; set; }
        [JsonIgnore]
        public long? IdCliente { get; set; }
        public GetClienteDto Cliente { get; set; }
        public List<GetDetalleVentasDto> DetalleVentas {get;set;}
    }
}