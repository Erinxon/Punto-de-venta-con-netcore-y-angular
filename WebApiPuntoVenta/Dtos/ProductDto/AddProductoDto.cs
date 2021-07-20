using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPuntoVenta.Dtos.ProductDto
{
    public class AddProductoDto
    {
        public string Codigo { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public decimal? Costo { get; set; }
        public decimal? Precio { get; set; }
        public int? Stock { get; set; }
        public long? IdCategoria { get; set; }
        public bool? Estatus { get; set; }
    }
}
