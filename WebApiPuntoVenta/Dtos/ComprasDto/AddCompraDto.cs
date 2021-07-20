using System;
using System.Collections.Generic;

namespace WebApiPuntoVenta.Dtos.ComprasDto
{
    public class AddCompraDto
    {
        public string EmailUsuario { get; set; }
        public int? IdProveedor { get; set; }
        public List<AddDetalleCompraDto> DetalleCompra {get;set;}
    }
}