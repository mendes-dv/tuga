using System;
using System.Collections.Generic;

namespace GestMiniMercados.Models;

public partial class Categorium
{
    public int IdCategoria { get; set; }

    public string NomeCategoria { get; set; } = null!;

    public double Iva { get; set; }

    public virtual ICollection<Fornecedor> Fornecedors { get; set; } = new List<Fornecedor>();

    public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}
