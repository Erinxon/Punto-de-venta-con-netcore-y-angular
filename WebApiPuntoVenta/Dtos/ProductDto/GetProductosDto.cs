using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebApiPuntoVenta.Dtos.CategoriaDto;

namespace WebApiPuntoVenta.Dtos.ProductDto
{
    public class GetProductosDto
    {
        public long Id { get; set; }
        public string Codigo { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public decimal? Costo { get; set; }
        public decimal? Precio { get; set; }
        public int? Stock { get; set; }
        [JsonIgnore]
        public long? IdCategoria { get; set; }
        public GetCategoriaDto Categoria { get; set; }
        public DateTime? FechaCreado { get; set; }
        public bool? Estatus { get; set; }
    }
}
