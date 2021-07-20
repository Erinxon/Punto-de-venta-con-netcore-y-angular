using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPuntoVenta.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Venta = new HashSet<Venta>();
        }

        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }
        public int? IdDireccion { get; set; }
        public DateTime? FechaCreado { get; set; }
        public bool? Estatus { get; set; }

        public virtual Direccion IdDireccionNavigation { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
