using System;

namespace WebApiPuntoVenta.Dtos.UsuarioDto
{
    public class AddUsuarioDto
    {
        public string Email { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Pass { get; set; }
        public string Roll { get; set; }
    }
}