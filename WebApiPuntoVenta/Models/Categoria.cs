using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPuntoVenta.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        public long Id { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaCreado { get; set; }
        public bool? Estatus { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
