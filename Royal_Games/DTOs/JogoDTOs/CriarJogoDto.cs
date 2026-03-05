namespace Royal_Games.DTOs.JogoDTOs
{
    public class CriarJogoDto
    {
        public string Nome { get; set; } = null!;
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = null!;
        public IFormFile Imagem { get; set; } = null!;
        public List<int> generoIds { get; set; } = new();
    }
}
