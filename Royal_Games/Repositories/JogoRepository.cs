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
            List<Jogo> jogos = _context.Jogo.Include(jogo => jogo.Genero).ToList();
            return jogos;
        }

        public Jogo ObterPorID(int id)
        {
            Jogo? jogo = _context.Jogo.
                Include(jogoDb => jogoDb.Genero).
                Include(jogoDb => jogoDb.ClassificacaoIndicativa).
                Include(jogoDb => jogoDb.Usuario).
                Include(jogoDb => jogoDb.Plataforma).
                FirstOrDefault(jogoDb => jogoDb.JogoID == id);

            return jogo;
        }

        public bool NomeExiste(string nome, int? JogoIdAtual = null)
        {
            var JogoConsultado = _context.Jogo.AsQueryable();

            if (JogoIdAtual.HasValue)
            {
                JogoConsultado = JogoConsultado.Where(jogo => jogo.JogoID != JogoIdAtual.Value);
            }

            return JogoConsultado.Any(jogo => jogo.Nome == nome);
        }

        public byte[] ObterImagem(int id)
        {
            byte[]? imagem = _context.Jogo
                .Where(jogo => jogo.JogoID == id)
                .Select(jogo => jogo.Imagem)
                .FirstOrDefault();

            return imagem;
        }

        public void Cadastrar(Jogo jogo, List<int> generoIds)
        {
            List<Genero> generos = _context.Genero
                .Where(genero => generoIds.Contains(genero.GeneroID))
                .ToList();

            jogo.Genero = generos;

            _context.Jogo.Add(jogo);
            _context.SaveChanges();
        }

        public void Atualizar(Jogo jogo, List<int> generoIds)
        {
            Jogo? jogoBanco = _context.Jogo
                .Include(j => j.Genero)
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

            var generos = _context.Genero.Where(genero => generoIds.Contains(genero.GeneroID)).ToList();

            jogoBanco.Genero.Clear();

            foreach (var genero in generos)
            {
                jogoBanco.Genero.Add(genero);
            }

            _context.SaveChanges();
        }
        

        public void Remover(int id)
        {
            Jogo? jogo = _context.Jogo.FirstOrDefault(jogo => jogo.JogoID == id);

            if(jogo != null)
            {
                return;
            }

            _context.Jogo.Remove(jogo);
            _context.SaveChanges();
        }

    }
}
