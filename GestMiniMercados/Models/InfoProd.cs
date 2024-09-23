using System;
using System.Collections.Generic;

namespace GestMiniMercados.Models;

public partial class InfoProd
{
    public string NomeProduto { get; set; } = null!;

    public int QtdAtualProduto { get; set; }

    public int QtdMaximaProduto { get; set; }

    public double PreçoVenda { get; set; }

    public double PreçoCompra { get; set; }

    public double MargemLucro { get; set; }

    public double PreçoCampanha { get; set; }
}
