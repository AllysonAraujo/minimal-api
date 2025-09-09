using System;
using System.Collections.Generic;

namespace mininal_api.Models;

public partial class Veiculo
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public int Ano { get; set; }
}
