using System;
using System.Collections.Generic;

namespace GestMiniMercados.Models;

public partial class Feria
{
    public long NContribuinte { get; set; }

    public DateTime DataInicioFerias { get; set; }

    public DateTime DataFimFerias { get; set; }
}
