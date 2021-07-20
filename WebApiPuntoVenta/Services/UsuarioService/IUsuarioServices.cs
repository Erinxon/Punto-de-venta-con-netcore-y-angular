using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiPuntoVenta.Dtos.UsuarioDto;
using WebApiPuntoVenta.Response;

namespace WebApiPuntoVenta.Services.UsuarioService
{
    public interface IUsuarioServices
    {
        Task<ServiceResponse<List<GetUsuarioDto>>> GetAllUsuario();
        Task<ServiceResponse<List<GetUsuarioDto>>> FilterUsuario(string search);
        Task<ServiceResponse<GetUsuarioDto>> GetUsuarioById(long id);
        Task<ServiceResponse<GetUsuarioDto>> UpdateEstatusUsuario(long id);
        Task<int> GetUsuarioByEmail(string email);
        Task<ServiceResponse<GetUsuarioDto>> AddUsuario(AddUsuarioDto newUsuario);
        Task<ServiceResponse<GetUsuarioDto>> Update(UpdateUsuarioDto updateUsuario);
        Task<ServiceResponse<UserResponse>> Autenticar(AuthUsuarioDto authUsuario);
    }
}