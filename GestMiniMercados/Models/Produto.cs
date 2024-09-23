using System;
using System.Collections.Generic;

namespace GestMiniMercados.Models;

public partial class Produto
{
    public int IdProduto { get; set; }

    public string NomeProduto { get; set; } = null!;

    public int IdCategoria { get; set; }

    public int QtdProduto { get; set; }

    public double PrecoUnitarioCompra { get; set; }

    public double PrecoUnitarioVenda { get; set; }

    public double MargemUnitaria { get; set; }

    public double PrecoCampanha { get; set; }

    public int QtdTotal { get; set; }

    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;
}
