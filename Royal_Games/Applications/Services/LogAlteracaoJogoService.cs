using Royal_Games.Domains;
using Royal_Games.DTOs.LogJogoDto;
using Royal_Games.Interfaces;


namespace Royal_Games.Applications.Services
{
    public class LogAlteracaoJogoService
    {
        private readonly ILogAlteracaoJogoRepository _repository;

        public LogAlteracaoJogoService(ILogAlteracaoJogoRepository repository)
        {
            _repository = repository;
        }

        public List<LerLogJogoDto> Listar()
        {
            List<Log_AlteracaoJogo> logs = _repository.Listar();

            List<LerLogJogoDto> listaLogJogo = logs.Select(logs => new LerLogJogoDto
            {
                LogID = logs.Log_AlteracaoJogoID,
                JogoID = logs.JogoID,
                DataAlteracao = logs.DataAlteracao,
                NomeAnterior = logs.NomeAnterior,
                PrecoAnterior = logs.PrecoAnterior
            }).ToList();    
            return listaLogJogo;
        }

        public List<LerLogJogoDto> ListarPorJogo(int JogoID)
        {
            List<Log_AlteracaoJogo> logs = _repository.ListarPorJogo(JogoID);

            List<LerLogJogoDto> listaLogJogo = logs.Select(logs => new LerLogJogoDto
            {
                LogID = logs.Log_AlteracaoJogoID,
                JogoID = logs.JogoID,
                DataAlteracao = logs.DataAlteracao,
                NomeAnterior = logs.NomeAnterior,
                PrecoAnterior = logs.PrecoAnterior
            }).ToList();
            return listaLogJogo;

        }

        public List<LerLogJogoDto> ListarPorProduto(int produtoId)
        {
            List<Log_AlteracaoJogo> logs = _repository.ListarPorJogo(produtoId);

            List<LerLogJogoDto> listaLogProduto = logs.Select(log => new LerLogJogoDto
            {
                DataAlteracao = log.DataAlteracao,
                JogoID = log.JogoID,
                LogID = log.Log_AlteracaoJogoID,
                NomeAnterior = log.NomeAnterior,
                PrecoAnterior = log.PrecoAnterior
            }).ToList();

            return listaLogProduto;
        }

    }
}
