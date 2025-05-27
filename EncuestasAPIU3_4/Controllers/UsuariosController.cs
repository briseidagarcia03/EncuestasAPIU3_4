using EncuestasAPIU3_4.Models.DTOs;
using EncuestasAPIU3_4.Models.Entities;
using EncuestasAPIU3_4.Models.Validators;
using EncuestasAPIU3_4.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EncuestasAPIU3_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public UsuariosController(Repository<Usuarios> repository, UsuarioValidator validador)
        {
            Repository = repository;
            Validador = validador;
        }

        public Repository<Usuarios> Repository { get; }
        public UsuarioValidator Validador { get; }

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
    }
}
