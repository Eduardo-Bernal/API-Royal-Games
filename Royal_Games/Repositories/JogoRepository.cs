using Microsoft.EntityFrameworkCore;
using Royal_Games.Contexts;
using Royal_Games.Domains;
using Royal_Games.Interfaces;

namespace Royal_Games.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly Royal_GamesContext _context;

        public JogoRepository(Royal_GamesContext context)
        {
            _context = context;
        }

        public List<Jogo> Listar()
        {
<<<<<<< HEAD
            List<Jogo> jogos = _context.Jogo
                .Include(jogo => jogo.Genero)
                .Include(jogo => jogo.ClassificacaoIndicativa)
                .Include(jogo => jogo.Usuario)
                .Include(jogo => jogo.Plataforma)
                .ToList();

=======
            List<Jogo> jogos = _context.Jogos.Include(jogo => jogo.Generos).ToList();
>>>>>>> a4163a5d20d366d5f0dad022684a82a90d6272c7
            return jogos;
        }

        public Jogo ObterPorID(int id)
        {
<<<<<<< HEAD
            Jogo? jogo = _context.Jogo
                .Include(jogoDb => jogoDb.Genero)
                .Include(jogoDb => jogoDb.ClassificacaoIndicativa)
                .Include(jogoDb => jogoDb.Usuario)
                .Include(jogoDb => jogoDb.Plataforma)
                .FirstOrDefault(jogoDb => jogoDb.JogoID == id);
=======
            Jogo? jogo = _context.Jogos.
                Include(jogoDb => jogoDb.Generos).
                Include(jogoDb => jogoDb.ClassificacaoIndicativa).
                Include(jogoDb => jogoDb.Usuario).
                Include(jogoDb => jogoDb.Plataformas).
                FirstOrDefault(jogoDb => jogoDb.JogoID == id);
>>>>>>> a4163a5d20d366d5f0dad022684a82a90d6272c7

            return jogo;
        }

        public bool NomeExiste(string nome, int? JogoIdAtual = null)
        {
            var JogoConsultado = _context.Jogos.AsQueryable();

            if (JogoIdAtual.HasValue)
            {
                JogoConsultado = JogoConsultado.Where(jogo => jogo.JogoID != JogoIdAtual.Value);
            }

            return JogoConsultado.Any(jogo => jogo.Nome == nome);
        }

        public byte[] ObterImagem(int id)
        {
            byte[]? imagem = _context.Jogos
                .Where(jogo => jogo.JogoID == id)
                .Select(jogo => jogo.Imagem)
                .FirstOrDefault();

            return imagem;
        }

        public void Cadastrar(Jogo jogo, List<int> generoIds)
        {
            List<Genero> generos = _context.Generos
                .Where(genero => generoIds.Contains(genero.GeneroID))
                .ToList();

            jogo.Generos = generos;

            _context.Jogos.Add(jogo);
            _context.SaveChanges();
        }

        public void Atualizar(Jogo jogo, List<int> generoIds)
        {
            Jogo? jogoBanco = _context.Jogos
                .Include(j => j.Generos)
                .FirstOrDefault(jogoAux => jogoAux.JogoID == jogo.JogoID);

            if (jogoBanco == null)
            {
                return;
            }

            jogoBanco.Nome = jogo.Nome;
            jogoBanco.Preco = jogo.Preco;
            jogoBanco.Descricao = jogo.Descricao;

            if (jogo.Imagem != null && jogo.Imagem.Length > 0)
            {
                jogoBanco.Imagem = jogo.Imagem;
            }

            if (jogo.StatusJogo.HasValue)
            {
                jogoBanco.StatusJogo = jogo.StatusJogo;
            }

<<<<<<< HEAD
            var generos = _context.Genero
                .Where(genero => generoIds.Contains(genero.GeneroID))
                .ToList();
=======
            var generos = _context.Generos.Where(genero => generoIds.Contains(genero.GeneroID)).ToList();
>>>>>>> a4163a5d20d366d5f0dad022684a82a90d6272c7

            jogoBanco.Generos.Clear();

            foreach (var genero in generos)
            {
                jogoBanco.Generos.Add(genero);
            }

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Jogo? jogo = _context.Jogos.FirstOrDefault(jogo => jogo.JogoID == id);

            if (jogo == null)
            {
                return;
            }

            _context.Jogos.Remove(jogo);
            _context.SaveChanges();
        }
    }
}