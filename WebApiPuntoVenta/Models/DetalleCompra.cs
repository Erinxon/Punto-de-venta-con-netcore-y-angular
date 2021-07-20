using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPuntoVenta.Models
{
    public partial class DetalleCompra
    {
        public int Id { get; set; }
        public decimal? Costo { get; set; }
        public int? Cantidad { get; set; }
        public long? IdCompra { get; set; }
        public long? IdProducto { get; set; }

        public virtual Compra IdCompraNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
