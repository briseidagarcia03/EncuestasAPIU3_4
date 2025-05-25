using System;
using System.Collections.Generic;

namespace EncuestasAPIU3_4.Models.Entities;

public partial class Encuestas
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public int IdUsuario { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<AplicacionEncuesta> AplicacionEncuesta { get; set; } = new List<AplicacionEncuesta>();

    public virtual Usuarios IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Preguntas> Preguntas { get; set; } = new List<Preguntas>();
}
