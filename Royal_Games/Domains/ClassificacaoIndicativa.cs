using System;
using System.Collections.Generic;

namespace Royal_Games.Domains;

public partial class ClassificacaoIndicativa
{
    public int ClassificacaoIndicativaID { get; set; }

    public string? Classificacao { get; set; }

    public virtual ICollection<Jogo> Jogo { get; set; } = new List<Jogo>();
}
