using System;
using System.Collections.Generic;

namespace EncuestasAPIU3_4.Models.Entities;

public partial class Usuarios
{
    public int Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public string Rol { get; set; } = null!;

    public virtual ICollection<AplicacionEncuesta> AplicacionEncuesta { get; set; } = new List<AplicacionEncuesta>();

    public virtual ICollection<Encuestas> Encuestas { get; set; } = new List<Encuestas>();
}
