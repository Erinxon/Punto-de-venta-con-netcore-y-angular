using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPuntoVenta.Models
{
    public partial class DetalleVenta
    {
        public int Id { get; set; }
        public decimal? Precio { get; set; }
        public int? Cantidad { get; set; }
        public long? IdVenta { get; set; }
        public long? IdProducto { get; set; }

        public virtual Producto IdProductoNavigation { get; set; }
        public virtual Venta IdVentaNavigation { get; set; }
    }
}
