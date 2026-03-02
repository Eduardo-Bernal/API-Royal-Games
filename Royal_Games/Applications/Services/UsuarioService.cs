using System.Security.Cryptography;
using System.Text;
using Royal_Games.Domains;
using Royal_Games.Interfaces;
using Royal_Games.Exceptions;
using Royal_Games.DTOs.UsuarioDto;

namespace Royal_Games.Applications.Services
{
    public class UsuarioService
    {

        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _repository = usuarioRepository;
        }

        private static LerUsuarioDto LerDto(Usuario usuario)
        {
            LerUsuarioDto lerUsuarioDto = new LerUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                StatusUsuario = usuario.StatusUsuario ?? false
            };
            return lerUsuarioDto;
        }

        public List<LerUsuarioDto> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();

            List<LerUsuarioDto> usuariosDto = usuarios.Select(usuarioBanco => LerDto(usuarioBanco)).ToList();
            return usuariosDto;
        }

        private static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new DomainException("Email Inválido.");
            }
        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome Inválido.");
            }
        }


        private static byte[] HashSenha(string senha)
        {
            if (string.IsNullOrEmpty(senha))
            {
                throw new DomainException("Senha é obrigatória.");
            }
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        public LerUsuarioDto ObterPorId(int id)
        {
            Usuario usuario = _repository.ObterPorId(id);

            if (usuario == null)
            {
                throw new DomainException("Usuário não existe.");
            }

            return LerDto(usuario);
        }
        public LerUsuarioDto ObterPorEmail(string email)
        {
            Usuario usuario = _repository.ObterPorEmail(email);

            if (usuario == null)
            {
                throw new DomainException("Usuário não existe.");
            }

            return LerDto(usuario);
        }

        public LerUsuarioDto Adicionar(CriarUsuarioDto usuarioDto)
        {
            ValidarEmail(usuarioDto.Email);
            ValidarNome(usuarioDto.Nome);

            if (_repository.EmailExiste(usuarioDto.Email)) throw new DomainException("Email já cadastrado!");

            Usuario usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = HashSenha(usuarioDto.Senha),
                StatusUsuario = true,
            };

            _repository.Adicionar(usuario);

            return LerDto(usuario);
        }

        public LerUsuarioDto Atualizar(int id, CriarUsuarioDto usuarioDto)
        {
            Usuario usuarioBanco = _repository.ObterPorId(id);

            if (usuarioBanco == null) throw new DomainException("Usuário não foi encontrado!");

            ValidarEmail(usuarioDto.Email);

            Usuario usuarioComMesmoEmail = _repository.ObterPorEmail(usuarioDto.Email);

            if (usuarioComMesmoEmail != null && usuarioComMesmoEmail.UsuarioID != id)
            {
                throw new DomainException("Já existe um usuário com este email");
            }

            usuarioBanco.Nome = usuarioDto.Nome;
            usuarioBanco.Email = usuarioDto.Email;
            usuarioBanco.Senha = HashSenha(usuarioDto.Senha);

            _repository.Atualizar(usuarioBanco);

            return LerDto(usuarioBanco);
        }

        public void Remover(int id)
        {
            Usuario usuario = _repository.ObterPorId(id);

            if (usuario == null) throw new DomainException("Usuário não encontrado");

            _repository.Remover(id);
        }

    }
}
