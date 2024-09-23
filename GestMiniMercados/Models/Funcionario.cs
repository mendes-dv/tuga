using System;
using System.Collections.Generic;
using GestMiniMercados.Models;

namespace GestMiniMercados;

public partial class Funcionario
{
    public string NContribuinte { get; set; } = null!;

    public string? Nome { get; set; }

    public virtual ICollection<Feria> Feria { get; set; } = new List<Feria>();

    public virtual ICollection<HorarioDeTrabalho> HorarioDeTrabalhos { get; set; } = new List<HorarioDeTrabalho>();
}
