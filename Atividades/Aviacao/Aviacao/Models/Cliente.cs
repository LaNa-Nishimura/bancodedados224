using System;
using System.Collections.Generic;

namespace Aviacao.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nome { get; set; } = null!;

    public string Contato { get; set; } = null!;

    public string Genero { get; set; } = null!;

    public DateTime DataNascimento { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
