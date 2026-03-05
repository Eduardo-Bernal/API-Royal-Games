using Royal_Games.Contexts;
using Royal_Games.Domains;
using Royal_Games.Exceptions;
using Royal_Games.Interfaces;

namespace Royal_Games.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly Royal_GamesContext _context;

        public GeneroRepository(Royal_GamesContext context)
        {
                       _context = context;
        }

        public List<Genero> Listar()
        {
            return _context.Generos.ToList();
        }

        public Genero ObterPorID(int id)
        {
            Genero? genero = _context.Generos.FirstOrDefault(g => g.GeneroID == id);
            return genero;
        }

        public bool NomeExiste(string nome, int? generoIdAtual = null)
        {
            var consulta = _context.Generos.AsQueryable();

            if (generoIdAtual.HasValue)
            {
                consulta = consulta.Where(categoria => categoria.GeneroID != generoIdAtual.Value);
            }

            return consulta.Any(c => c.Nome == nome);
        }

        public void Adicionar(Genero genero)
        {
            _context.Generos.Add(genero);
            _context.SaveChanges();
        }

        public void Atualizar(Genero genero)
        {
            Genero? generoBanco = _context.Generos.FirstOrDefault(g => g.GeneroID == genero.GeneroID);

            if(generoBanco == null)
            {
                return;
            }

            generoBanco.Nome = genero.Nome;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Genero? generoBanco = _context.Generos.FirstOrDefault(g => g.GeneroID == id);

            if(generoBanco != null)
            {
                return;
            }

            _context.Generos.Remove(generoBanco);
            _context.SaveChanges();
        }
    }
}
