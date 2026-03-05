using Royal_Games.Domains;
using Royal_Games.DTOs.JogoDTOs;

namespace Royal_Games.Applications.Conversoes
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

                GeneroIds = jogo.Generos.Select(genero => genero.GeneroID).ToList(),

                Generos = jogo.Generos.Select(genero => genero.Nome).ToList(),

                UsuarioID = jogo.UsuarioID,
                UsuarioEmail = jogo.Usuario?.Email,
                UsuarioNome = jogo.Usuario?.Nome
            };
        }
    }
}