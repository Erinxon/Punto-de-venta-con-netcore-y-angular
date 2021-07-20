using System;
using System.Collections.Generic;

namespace WebApiPuntoVenta.Dtos.VentasDto
{
    public class AddVentasDto
    {
        public string EmailUsuario { get; set; }
        public long? IdCliente { get; set; }
        public List<AddDetalleVentasDto> DetalleVentas {get;set;}
    }
}