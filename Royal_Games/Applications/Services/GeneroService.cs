using Royal_Games.Domains;
using Royal_Games.DTOs.GeneroDto;
using Royal_Games.DTOs.JogoDTOs;
using Royal_Games.Exceptions;
using Royal_Games.Interfaces;

namespace Royal_Games.Applications.Services
{
    public class GeneroService
    {
        private readonly IGeneroRepository _repository;

        public GeneroService(IGeneroRepository repository)
        {
            _repository = repository;
        }

        public List<LerGeneroDto> Listar()
        {
            List<Genero> categorias = _repository.Listar();

           
            List<LerGeneroDto> categoriaDto = categorias.Select(categoria => new LerGeneroDto
            {
               GeneroID = categoria.GeneroID,
                Nome = categoria.Nome
            }).ToList();

            
            return categoriaDto;
        }

        public LerGeneroDto ObterPorID(int id)
        {
            Genero genero = _repository.ObterPorID(id);
            if (genero == null)
            {
                throw new DomainException("Genero não encontrado");
            }
            LerGeneroDto generoDto = new LerGeneroDto
            {
                GeneroID = genero.GeneroID,
                Nome = genero.Nome
            };
            return generoDto;
        }

        public void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome do gênero é obrigatório.");
            }
        }

        public void Adicionar(CriarGeneroDto criarGeneroDto)
        {
            ValidarNome(criarGeneroDto.Nome);

            if (_repository.NomeExiste(criarGeneroDto.Nome))
            {
                throw new DomainException("Já existe um gênero com esse nome.");
            }

            Genero genero = new Genero
            {
                Nome = criarGeneroDto.Nome
            };

            _repository.Adicionar(genero);
        }

        public void Atualizar(int id, CriarGeneroDto generoDto)
        {
            ValidarNome(generoDto.Nome);

            Genero generoBanco = _repository.ObterPorID(id);
            if (generoBanco == null)
            {
                throw new DomainException("Gênero não encontrado.");
            }

            if (_repository.NomeExiste(generoDto.Nome, generoIdAtual: id))
            {
                throw new DomainException("Já existe outro gênero com esse nome.");
            }

            generoBanco.Nome = generoDto.Nome;
            _repository.Atualizar(generoBanco);
        }

        public void Remover(int id)
        {
            Genero generoBanco = _repository.ObterPorID(id);
            if (generoBanco == null)
            {
                throw new DomainException("Gênero não encontrado");
            }

            _repository.Remover(id);
        }
    }
}
