namespace GestMiniMercados.Models;

public partial class CategoriaPerc
{
    public string NomeCategoria { get; set; } = null!;

    public double Percentagem { get; set; }

    public double Lucro { get; set; }
}
