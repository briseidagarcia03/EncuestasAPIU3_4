using System;
using System.Collections.Generic;

namespace EncuestasAPIU3_4.Models.Entities;

public partial class AplicacionEncuesta
{
    public int Id { get; set; }

    public int IdEncuesta { get; set; }

    public int IdUsuario { get; set; }

    public DateTime FechaAplicacion { get; set; }

    public virtual ICollection<Entrevistados> Entrevistados { get; set; } = new List<Entrevistados>();

    public virtual Encuestas IdEncuestaNavigation { get; set; } = null!;

    public virtual Usuarios IdUsuarioNavigation { get; set; } = null!;
}
