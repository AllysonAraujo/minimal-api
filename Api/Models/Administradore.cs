using System;
using System.Collections.Generic;

namespace mininal_api.Models;

public partial class Administradore
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public string Perfil { get; set; } = null!;
}
