using System;
using System.Collections.Generic;

namespace GestMiniMercados.Models;

public partial class User
{
    public long NContribuinte { get; set; }

    public string NomeFuncionario { get; set; } = null!;

    public string TpUtilizador { get; set; } = null!;

    public DateTime DataContrato { get; set; }

    public long Telemovel { get; set; }

    public string Morada { get; set; } = null!;

    public string EstadoCivil { get; set; } = null!;

    public DateTime DataNascimento { get; set; }

    public string Password { get; set; } = null!;

    public byte[]? Foto { get; set; }
}
