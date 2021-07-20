using System;
using System.Collections.Generic;

namespace WebApiPuntoVenta.Dtos.VentasDto
{
    public class AddDetalleVentasDto
    {
        public decimal? Precio { get; set; }
        public int? Cantidad { get; set; }
        public long? IdProducto { get; set; }
    }
}