using Royal_Games.Interfaces;
using Royal_Games.DTOs.JogoDTOs;
using Royal_Games.Domains;
using Royal_Games.Applications.Conversoes;
using Royal_Games.Exceptions;
using VHBurguer.Applications.Conversoes;
using Royal_Games.Applications.Regras;

namespace Royal_Games.Applications.Services
{
    public class JogoService
    {
        private readonly IJogoRepository _repository;

        public JogoService(IJogoRepository repository)
        {
            _repository = repository;
        }

        public List<LerJogoDto> Listar()
        {
            List<Jogo> jogos = _repository.Listar();
            List<LerJogoDto> jogosDto = jogos.Select(JogoParaDto.ConverterParaDto).ToList();
            return jogosDto;
        }

        public LerJogoDto ObterPorID(int id)
        {
            Jogo jogo = _repository.ObterPorID(id);
            if (jogo == null)
            {
                throw new DomainException("jogo não encontrado.");
            }

            return JogoParaDto.ConverterParaDto(jogo);
        }

        private static void ValidarCadastro(CriarJogoDto jogoDto)
        {
            if (string.IsNullOrEmpty(jogoDto.Nome))
            {
                throw new DomainException("nome do jogo é obrigatório.");
            }
            if (jogoDto.Preco < 0)
            {
                throw new DomainException("O preço do jogo deve ser maior que zero");
            }
            if (string.IsNullOrEmpty(jogoDto.Descricao))
            {
                throw new DomainException("O jogo precisa ter uma descrição");
            }
            if (jogoDto.Imagem == null || jogoDto.Imagem.Length == 0)
            {
                throw new DomainException("O jogo precisa ter uma imagem");
            }
            if (jogoDto.generoIds == null || jogoDto.generoIds.Count == 0)
            {
                throw new DomainException("O jogo deve ter ao menos um genero");
            }

            public byte[] ObterImagem(int id)
        {
            var Imagem = _repository.ObterImagem(id);

            if(Imagem == null || Imagem.Length == 0)
            {
                throw new DomainException("Imagem não encontrada");
            }
            return Imagem;
        }

        public LerJogoDto Adicionar(CriarJogoDto jogoDto, int usuarioId)
        {
            ValidarCadastro(jogoDto);

            if(_repository.NomeExiste(jogoDto.Nome)
            {
                throw new DomainException("Jogo já existe");
            }

            Jogo jogo = new Jogo
            {
                Nome = jogo.Nome,
                Preco = jogo.Preco,
                Descricao = jogo.Descricao,
                Imagem = ImagemParaBytes.ConverterImagem(jogoDto.Imagem),
                StatusJogo = true,
                UsuarioID = usuarioId
            };

            _repository.Cadastrar(jogo, jogoDto.generoIds);
            return JogoParaDto.ConverterParaDto(jogo);
        }

        public LerJogoDto Atualizar(int id, AtualizarJogoDto jogoDto)
        {
            HorarioAlteracaoJogo.ValidarHorario();

            Jogo jogoBanco = _repository.ObterPorID(id);

            if(jogoBanco == null)
            {
                throw new DomainException("Jogo não encontrado.");
            }
            if(_repository.NomeExiste(jogoDto.Nome, JogoIdAtual:id)
            {
                throw new DomainException("Já existe outro produto com esse nome");
            }
            if (jogoDto.generoIds == null || jogoDto.generoIds.Count == 0)
            {
                throw new DomainException("Jogo deve ter ao menos um genero.");
            }
            if(jogoDto.Preco < 0)
            {
                throw new DomainException("O preço do jogo deve ser maior que 0.");
            }

            jogoBanco.Nome = jogoDto.Nome;
            jogoBanco.Preco = jogoDto.Preco;
            jogoBanco.Descricao = jogoDto.Descricao;

            if(jogoDto.Imagem != null && jogoDto.Imagem.Length > 0)
            {
                jogoBanco.Imagem = ImagemParaBytes.ConverterImagem(jogoDto.Imagem);
            }
            if(jogoDto.StatusJogo.HasValue)
            {
                jogoBanco.StatusJogo = jogoDto.StatusJogo;
            }

            _repository.Atualizar(jogoBanco, jogoDto.generoIds);
            return JogoParaDto.ConverterParaDto(jogoBanco);
        }

        public void Remover(int id)
        {
            Jogo jogo = _repository.ObterPorID(id);

            if(jogo == null)
            {
                throw new DomainException("Jogo não encontrado.");
            }

            _repository.Remover(id);
        }

        
        
    }
}
