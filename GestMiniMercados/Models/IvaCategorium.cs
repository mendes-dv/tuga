using System;
using System.Collections.Generic;

namespace GestMiniMercados.Models;

public partial class IvaCategorium
{
    public string NomeCategoria { get; set; } = null!;

    public double IvaCategoria { get; set; }
}
