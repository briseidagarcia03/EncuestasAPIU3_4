using EncuestasAPIU3_4.Models.DTOs;
using EncuestasAPIU3_4.Models.Entities;
using EncuestasAPIU3_4.Models.Validators;
using EncuestasAPIU3_4.Repositories;
using EncuestasAPIU3_4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EncuestasAPIU3_4.Controllers
{
    [AllowAnonymous] //el registro si necesita autoenticación
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public UsuariosController(Repository<Usuarios> repository, UsuarioValidator validador, JwtService service)
        {
            Repository = repository;
            Validador = validador;
            Service = service;
        }

        public Repository<Usuarios> Repository { get; }
        public UsuarioValidator Validador { get; }
        public JwtService Service { get; }

        [HttpPost]
        public IActionResult Registrar(UsuarioDTO dto)
        {
            if(Validador.Validate(dto, out List<string> errores))
            {
                Usuarios user = new()
                {
                    Contrasena = dto.Contrasena,
                    NombreUsuario = dto.Nombre,
                    Rol = dto.Rol,
                    FechaRegistro = DateTime.Now
                };
                Repository.Insert(user);
                return Ok();
            }
            else
            {
                return BadRequest(errores);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(UsuarioDTO dto)
        {
            var token = Service.GenerarToken(dto);

            if(token == null)
            {
                return Unauthorized("El usuario o contraseña son incorrectos.");
            }
            else
            {
                return Ok(token);
            }
        }
    }
}
