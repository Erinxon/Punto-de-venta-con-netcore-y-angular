using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPuntoVenta.Dtos.CategoriaDto
{
    public class AddCategoriaDto
    {
        public string Nombre { get; set; }
        public bool? Estatus { get; set; }
    }
}
