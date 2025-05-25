using System;
using System.Collections.Generic;

namespace EncuestasAPIU3_4.Models.Entities;

public partial class Entrevistados
{
    public int Id { get; set; }

    public int IdAplicacion { get; set; }

    public string Nombre { get; set; } = null!;

    public string NumControl { get; set; } = null!;

    public virtual AplicacionEncuesta IdAplicacionNavigation { get; set; } = null!;

    public virtual ICollection<Repuestas> Repuestas { get; set; } = new List<Repuestas>();
}
