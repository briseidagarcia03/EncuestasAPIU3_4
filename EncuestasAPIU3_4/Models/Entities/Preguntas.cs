using System;
using System.Collections.Generic;

namespace EncuestasAPIU3_4.Models.Entities;

public partial class Preguntas
{
    public int Id { get; set; }

    public int IdEncuesta { get; set; }

    public string Pregunta { get; set; } = null!;

    public int Orden { get; set; }

    public virtual Encuestas IdEncuestaNavigation { get; set; } = null!;

    public virtual ICollection<Repuestas> Repuestas { get; set; } = new List<Repuestas>();
}
