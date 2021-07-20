using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPuntoVenta.Dtos.CategoriaDto
{
    public class GetCategoriaDto
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaCreado { get; set; }
        public bool? Estatus { get; set; }
    }
}
