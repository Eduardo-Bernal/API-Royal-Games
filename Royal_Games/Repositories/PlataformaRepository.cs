using Royal_Games.Contexts;
using Royal_Games.Domains;
using Royal_Games.Interfaces;

namespace Royal_Games.Repositories
{
    public class PlataformaRepository : IPlataformaRepository
    {
        private readonly Royal_GamesContext _context;

        public PlataformaRepository(Royal_GamesContext context)
        {
            _context = context;
        }

        public void Adicionar(Plataforma plataforma)
        {
            _context.Plataformas.Add(plataforma);
            _context.SaveChanges();
        }

        public void Atualizar(Plataforma plataforma)
        {
            Plataforma plataformaBanco = _context.Plataformas.FirstOrDefault(p => p.PlataformaID == plataforma.PlataformaID);

            if (plataformaBanco == null)
            {
                return;
            }

            plataformaBanco.Nome = plataforma.Nome;

            _context.SaveChanges();
        }

        public List<Plataforma> Listar()
        {
            return _context.Plataformas.ToList();
        }

        public bool NomeExiste(string nome, int? plataformaIdAtual = null)
        {
            var consulta = _context.Plataformas.AsQueryable();

            if (plataformaIdAtual.HasValue)
            {
                consulta = consulta.Where(p => p.PlataformaID != plataformaIdAtual.Value);
            }

            return consulta.Any(c => c.Nome == nome);
        }

        public Plataforma ObterPorId(int id)
        {
            Plataforma plataforma = _context.Plataformas.FirstOrDefault(p => p.PlataformaID == id);

            return plataforma;
        }

        public void Remover(int id)
        {
            Plataforma plataforma = _context.Plataformas.FirstOrDefault(p => p.PlataformaID == id);

            if (plataforma == null)
            {
                return;
            }

            _context.Plataformas.Remove(plataforma);
            _context.SaveChanges();
        }
    }
}
