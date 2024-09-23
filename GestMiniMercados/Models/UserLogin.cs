using System;
using System.Collections.Generic;

namespace GestMiniMercados.Models;

public partial class UserLogin
{
    public long NContribuinte { get; set; }

    public string Password { get; set; } = null!;
}
