using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPuntoVenta.Models
{
    public partial class Empresa
    {
        public int Id { get; set; }
        public string Rnc { get; set; }
        public string Ncf { get; set; }
        public string RazonSocial { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime? FechaCreado { get; set; }
        public bool? Estatus { get; set; }
    }
}
