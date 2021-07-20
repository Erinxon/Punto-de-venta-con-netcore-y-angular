using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPuntoVenta.Models
{
    public partial class Compra
    {
        public Compra()
        {
            DetalleCompras = new HashSet<DetalleCompra>();
        }

        public long Id { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdProveedor { get; set; }

        public virtual Proveedore IdProveedorNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }
    }
}
