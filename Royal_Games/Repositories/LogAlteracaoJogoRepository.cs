using Royal_Games.Contexts;
using Royal_Games.Domains;
using Royal_Games.Interfaces;

namespace Royal_Games.Repositories
{
    public class LogAlteracaoJogoRepository : ILogAlteracaoJogoRepository
    {
        private readonly Royal_GamesContext _context;

        public LogAlteracaoJogoRepository (Royal_GamesContext context)
        {
            _context = context;
        }

        public List<Log_AlteracaoJogo> Listar()
        {
            List<Log_AlteracaoJogo> listaAlteracoes = _context.Log_AlteracaoJogos.OrderByDescending(l => l.DataAlteracao).ToList();

            return listaAlteracoes;

        }

        public List<Log_AlteracaoJogo> ListarPorJogo(int jogoId)
        {
            List<Log_AlteracaoJogo> alteracaoProduto = _context.Log_AlteracaoJogos.Where(l => l.JogoID == jogoId).OrderByDescending(l => l.DataAlteracao).ToList();

            return alteracaoProduto;
        }
    }
}
