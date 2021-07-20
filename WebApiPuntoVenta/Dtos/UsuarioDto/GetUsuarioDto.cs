using System;
using System.Text.Json.Serialization;

namespace WebApiPuntoVenta.Dtos.UsuarioDto
{
    public class GetUsuarioDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Pass { get; set; }
        public string Roll { get; set; }
        public DateTime? FechaCreado { get; set; }
        public bool? Estatus { get; set; }
    }
}