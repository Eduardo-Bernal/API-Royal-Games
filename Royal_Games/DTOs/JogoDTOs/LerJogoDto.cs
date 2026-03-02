namespace Royal_Games.DTOs.JogoDTOs
{
    public class LerJogoDto
    {
        public int JogoID { get; set; }
        public string Nome { get; set; }

        public decimal Preco { get; set; }

        public string Descricao { get; set; } = null!;

        public byte[] Imagem { get; set; } = null!;

        public bool? StatusJogo { get; set; }

        public List<int> GeneroIds { get; set; } = new();

        public List<string> Generos { get; set; } = new();

        public int? UsuarioID { get; set; }

        public string UsuarioNome { get; set; } = null!;

        public string UsuarioEmail { get; set; } = null!;

        public byte[] Senha { get; set; } = null!;

        public bool? StatusUsuario { get; set; }
    }
}
