using System;
using System.Collections.Generic;

namespace GestMiniMercados.Models;

public partial class InfoFornecedore
{
    public string NomeFornecedor { get; set; } = null!;

    public string NomeCategoriaFornecida { get; set; } = null!;

    public string Morada { get; set; } = null!;

    public long NContribuinte { get; set; }
}
