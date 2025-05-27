using EncuestasAPIU3_4.Models.DTOs;
using EncuestasAPIU3_4.Models.Entities;
using EncuestasAPIU3_4.Repositories;
using System.Text.RegularExpressions;

namespace EncuestasAPIU3_4.Models.Validators
{
    public class UsuarioValidator
    {
        private readonly Repository<Usuarios> repository;

        public UsuarioValidator(Repository<Usuarios> repository)
        {
            this.repository = repository;
        }

        public bool Validate(UsuarioDTO user, out List<string> errores)
        {
            errores = new List<string>();

            if (string.IsNullOrWhiteSpace(user.Nombre))
            {
                errores.Add("El nombre de usuario está vacío.");
            }

            if (string.IsNullOrWhiteSpace(user.Contrasena))
            {
                errores.Add("La contraseña está vacía");
            }

            if (user.Nombre.Length > 45)
            {
                errores.Add("El nombre de usuario debe tener una longitud máxima de 45 carácteres.");
            }

            if (user.Contrasena.Length > 20)
            {
                errores.Add("La contraseña debe tener una longitud máxima de 20 carácteres.");
            }

            if (repository.GetAll().Any(x => x.NombreUsuario == user.Nombre))
            {
                errores.Add("Ya existe un usuario con el mismo nombre.");
            }

            if (!Regex.IsMatch(user.Contrasena, @"^(?=.*[A-ZÑ])(?=.*[a-zñ]).*$"))
            {
                errores.Add("La contraseña debe tener AL MENOS una letra mayúscula y una letra minúscula.");
            }

            return errores.Count == 0;


        }
    }
}
