using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPuntoVenta.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleCompras = new HashSet<DetalleCompra>();
            DetalleVenta = new HashSet<DetalleVenta>();
        }

        public long Id { get; set; }
        public string Codigo { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public decimal? Costo { get; set; }
        public decimal? Precio { get; set; }
        public int? Stock { get; set; }
        public long? IdCategoria { get; set; }
        public DateTime? FechaCreado { get; set; }
        public bool? Estatus { get; set; }

        public virtual Categoria IdCategoriaNavigation { get; set; }
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }
        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }
    }
}
