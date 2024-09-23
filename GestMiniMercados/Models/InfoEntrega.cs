using System;
using System.Collections.Generic;

namespace GestMiniMercados;

public partial class InfoEntrega
{
    public string NomeFornecedor { get; set; } = null!;

    public string NomeProduto { get; set; } = null!;

    public DateOnly DataEntrega { get; set; }
}
