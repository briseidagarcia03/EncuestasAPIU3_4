using EncuestasAPIU3_4.Models.DTOs;
using EncuestasAPIU3_4.Models.Entities;
using EncuestasAPIU3_4.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EncuestasAPIU3_4.Services
{
    public class JwtService
    {
        public JwtService(IConfiguration configuration, Repository<Usuarios> repository)
        {
            Configuration = configuration;
            Repository = repository;
        }

        public IConfiguration Configuration { get; }
        public Repository<Usuarios> Repository { get; }

        public string? GenerarToken(LoginDTO dto)
        {
            var usuario = Repository.GetAll().FirstOrDefault(x => x.NombreUsuario == dto.Nombre && x.Contrasena == dto.Contrasena);

            if(usuario == null)
            {
                return null;
            }
            else
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim("Id", usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                    new Claim(ClaimTypes.Role, usuario.Rol)
                };

                var descriptor = new JwtSecurityToken(
                    issuer: Configuration["Jwt:Issuer"],
                    audience: Configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(5),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256)
                );

                var handler = new JwtSecurityTokenHandler();
                return handler.WriteToken(descriptor);
            }
        }
    }
}
