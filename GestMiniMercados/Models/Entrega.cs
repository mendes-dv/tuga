using System;
using System.Collections.Generic;

namespace GestMiniMercados.Models;

public partial class Entrega
{
    public int IdFornecedor { get; set; }

    public int IdProduto { get; set; }

    public DateTime DataEntrega { get; set; }
}
