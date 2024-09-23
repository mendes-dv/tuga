using System;
using System.Collections.Generic;

namespace GestMiniMercados.Models;

public partial class Fornecedor
{
    public int IdFornecedor { get; set; }

    public string NomeFornecedor { get; set; } = null!;

    public int IdCategoria { get; set; }

    public string Morada { get; set; } = null!;

    public long NContribuinte { get; set; }

    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;
}
