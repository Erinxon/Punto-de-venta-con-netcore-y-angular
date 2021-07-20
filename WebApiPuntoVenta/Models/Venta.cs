using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPuntoVenta.Models
{
    public partial class Venta
    {
        public Venta()
        {
            DetalleVenta = new HashSet<DetalleVenta>();
        }

        public long Id { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdUsuario { get; set; }
        public long? IdCliente { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }
    }
}
