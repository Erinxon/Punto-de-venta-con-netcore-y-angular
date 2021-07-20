using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPuntoVenta.Dtos.EmpresaDto
{
    public class UpdateEmpresaDto
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
