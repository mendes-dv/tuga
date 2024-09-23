using System;
using System.Collections.Generic;

namespace GestMiniMercados.Models;

public partial class ProdsPerCat
{
    public string NomeCategoria { get; set; } = null!;

    public int QtdProds { get; set; }
}
