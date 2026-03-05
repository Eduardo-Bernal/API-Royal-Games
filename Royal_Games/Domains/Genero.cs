using System;
using System.Collections.Generic;

namespace Royal_Games.Domains;

public partial class Genero
{
    public int GeneroID { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Jogo> Jogos { get; set; } = new List<Jogo>();
}
