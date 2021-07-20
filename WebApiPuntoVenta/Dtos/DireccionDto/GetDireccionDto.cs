using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPuntoVenta.Dtos.DireccionDto
{
    public class GetDireccionDto
    {
        public int Id { get; set; }
        public string Provincia { get; set; }
        public string Ciudad { get; set; }
        public string Calle { get; set; }
        public string NumeroCasa { get; set; }
    }
}
