using System;

namespace WebApiPuntoVenta.Dtos.DireccionDto
{
    public class AddDireccionDto
    {
        public string Provincia { get; set; }
        public string Ciudad { get; set; }
        public string Calle { get; set; }
        public string NumeroCasa { get; set; }
    }
}