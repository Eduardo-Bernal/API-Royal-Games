using Royal_Games.Domains;
using Royal_Games.DTOs.JogoDTOs;

namespace VHBurguer.Applications.Conversoes
{
    public class JogoParaDto
    {
        public static LerJogoDto ConverterParaDto(Jogo jogo)
        {
            return new LerJogoDto
            {
                JogoID = jogo.JogoID,
                Nome = jogo.Nome,
                Preco = jogo.Preco,
                Descricao = jogo.Descricao,
                StatusJogo = jogo.StatusJogo,

                GeneroIds = jogo.Genero?.Select(g => g.GeneroID).ToList() ?? new List<int>(),
                Generos = jogo.Genero?.Select(g => g.Nome).ToList() ?? new List<string>(),

                UsuarioID = jogo.UsuarioID,
                UsuarioNome = jogo.Usuario?.Nome,
                UsuarioEmail = jogo.Usuario?.Email
            };
        }
    }
}