using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using WebApiPuntoVenta.Dtos.ProveedorDto;
using WebApiPuntoVenta.Dtos.UsuarioDto;

namespace WebApiPuntoVenta.Dtos.ComprasDto
{
    public class GetCompraDto
    {
        public long Id { get; set; }
        public DateTime? Fecha { get; set; }
        [JsonIgnore]
        public int? IdUsuario { get; set; }
        public GetUsuarioDto Usuario { get; set; }
        [JsonIgnore]
        public int? IdProveedor { get; set; }
        public GetProveedorDto Proveedor { get; set; }
        public List<GetDetalleCompraDto> DetalleCompra {get;set;}

    }
}