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

<<<<<<< HEAD
                GeneroIds = jogo.Genero?.Select(g => g.GeneroID).ToList() ?? new List<int>(),
                Generos = jogo.Genero?.Select(g => g.Nome).ToList() ?? new List<string>(),

                UsuarioID = jogo.UsuarioID,
                UsuarioNome = jogo.Usuario?.Nome,
                UsuarioEmail = jogo.Usuario?.Email
=======
                GeneroIds = jogo.Generos.Select(genero => genero.GeneroID).ToList(),

                Generos = jogo.Generos.Select(genero => genero.Nome).ToList(),

                UsuarioID = jogo.UsuarioID,
                UsuarioEmail = jogo.Usuario?.Email,
                UsuarioNome = jogo.Usuario?.Nome
>>>>>>> a4163a5d20d366d5f0dad022684a82a90d6272c7
            };
        }
    }
}