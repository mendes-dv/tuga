using System;
using System.Collections.Generic;

namespace GestMiniMercados.Models;

public partial class HorarioDeTrabalho
{
    public long NContribuinte { get; set; }

    public DateTime DataInicioTrabalho { get; set; }

    public DateTime DataFimTrabalho { get; set; }
}
