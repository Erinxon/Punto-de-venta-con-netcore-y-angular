using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPuntoVenta.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Compras = new HashSet<Compra>();
            Venta = new HashSet<Venta>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Pass { get; set; }
        public string Roll { get; set; }
        public DateTime? FechaCreado { get; set; }
        public bool? Estatus { get; set; }

        public virtual ICollection<Compra> Compras { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
