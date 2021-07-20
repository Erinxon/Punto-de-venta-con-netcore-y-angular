using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPuntoVenta.Models
{
    public partial class Proveedore
    {
        public Proveedore()
        {
            Compras = new HashSet<Compra>();
        }

        public int Id { get; set; }
        public string Rnc { get; set; }
        public string RazonSocial { get; set; }
        public string Telefono { get; set; }
        public int? IdDireccion { get; set; }
        public DateTime? FechaCreado { get; set; }
        public bool? Estatus { get; set; }

        public virtual Direccion IdDireccionNavigation { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
    }
}
