using System;
using System.Collections.Generic;

namespace EncuestasAPIU3_4.Models.Entities;

public partial class Repuestas
{
    public int Id { get; set; }

    public int IdEntrevistado { get; set; }

    public int IdPregunta { get; set; }

    public int Valor { get; set; }

    public virtual Entrevistados IdEntrevistadoNavigation { get; set; } = null!;

    public virtual Preguntas IdPreguntaNavigation { get; set; } = null!;
}
