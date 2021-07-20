using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiPuntoVenta.Dtos.UsuarioDto;
using WebApiPuntoVenta.Response;
using WebApiPuntoVenta.Models;
using WebApiPuntoVenta.Models.Auth;
using System;
using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace WebApiPuntoVenta.Services.UsuarioService
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly PuntoDeVentaContext _Context;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSetting;

        public UsuarioServices(PuntoDeVentaContext context, IMapper mapper, 
         IOptions<AppSettings> appSettings)
        {
            _mapper = mapper;
            _Context = context;
            _appSetting = appSettings.Value;
        }
        public async Task<ServiceResponse<GetUsuarioDto>> AddUsuario(AddUsuarioDto newUsuario)
        {
            var serviceResponse = new ServiceResponse<GetUsuarioDto>();
            try
            {
                Usuario usuario = _mapper.Map<Usuario>(newUsuario);
                usuario.FechaCreado = DateTime.Now.Date;
                usuario.Estatus = true;
                usuario.Pass =  Encrypt.GetSHA256(usuario.Pass);
                _Context.Usuarios.Add(usuario);
                await _Context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetUsuarioDto>(usuario);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<UserResponse>> Autenticar(AuthUsuarioDto authUsuario)
        {
            var serviceResponse = new ServiceResponse<UserResponse>();
            string password = Encrypt.GetSHA256(authUsuario.Password);

            try
            {
                Usuario usuario = await _Context.Usuarios.Where(u => u.Email == authUsuario.Email
                && u.Pass == password).FirstOrDefaultAsync();

                if (usuario == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Usuario o contraseña o incorrecta!";
                }
                else if ((bool)!usuario.Estatus)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "El usuario no está activo, por favor comuniquese con el administrador!";
                }
                else
                {
                    serviceResponse.Data = new UserResponse
                    {
                        Email = usuario.Email,
                        Token = GenerarToken(usuario)
                    };
                    serviceResponse.Message = "Autenticado";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
           
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUsuarioDto>>> FilterUsuario(string search)
        {
            var serviceResponse = new ServiceResponse<List<GetUsuarioDto>>();
            try
            {     
                serviceResponse.Data = await _Context.Usuarios.Where(x=>x.Email.Contains(search)
                || x.NombreUsuario.Contains(search) || x.Nombre.Contains(search) || x.Apellido.Contains(search)
                || (x.Nombre + " " + x.Apellido).Contains(search) || x.Roll.Contains(search))
                    .Select(u => _mapper.Map<GetUsuarioDto>(u)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUsuarioDto>>> GetAllUsuario()
        {
            var serviceResponse = new ServiceResponse<List<GetUsuarioDto>>();
            try
            {
                serviceResponse.Data = await _Context.Usuarios
                    .Select(u => _mapper.Map<GetUsuarioDto>(u)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<int> GetUsuarioByEmail(string email)
        {
            Usuario usuario = await _Context.Usuarios.Where(x => x.Email == email).FirstOrDefaultAsync();
            return usuario.Id;
        }

        public async Task<ServiceResponse<GetUsuarioDto>> GetUsuarioById(long id)
        {
            var serviceResponse = new ServiceResponse<GetUsuarioDto>();
            try
            {
                Usuario usuario = await _Context.Usuarios.Where(x=>x.Id == id).FirstOrDefaultAsync();
                serviceResponse.Data = _mapper.Map<GetUsuarioDto>(usuario);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUsuarioDto>> Update(UpdateUsuarioDto updateUsuario)
        {
            var serviceResponse = new ServiceResponse<GetUsuarioDto>();
            try
            {
                Usuario usuario = _mapper.Map<Usuario>(updateUsuario);
                _Context.Attach(usuario).State = EntityState.Modified;
                await _Context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetUsuarioDto>(usuario);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUsuarioDto>> UpdateEstatusUsuario(long id)
        {
            var serviceResponse = new ServiceResponse<GetUsuarioDto>();
            try
            {
                var usuario = await _Context.Usuarios.Where(x => x.Id == id).FirstOrDefaultAsync();
                usuario.Estatus = !usuario.Estatus;

                _Context.Attach(usuario).State = EntityState.Modified;
                await _Context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetUsuarioDto>(usuario);
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        private string GenerarToken(Usuario usuario){
            var jwtHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSetting.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier,usuario.Id.ToString()),
                    new Claim(ClaimTypes.Email,usuario.Email),
                    new Claim(ClaimTypes.Role,usuario.Roll)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtHandler.CreateToken(tokenDescriptor);
            return jwtHandler.WriteToken(token);
        }
    }
}