using Royal_Games.Interfaces;
using Royal_Games.Domains;
using Royal_Games.DTOs.AutenticacaoDto;

using Royal_Games.Applications.Autenticacao;

namespace Royal_Games.Applications.Services
{
    public class AutenticacaoService
    {
        private readonly IUsuarioRepository _repository;
        private readonly GeradorTokenJwt _tokenJwt;

        public AutenticacaoService(IUsuarioRepository repository, GeradorTokenJwt geradorTokenJwt)
        {
            _repository = repository;
            _tokenJwt = geradorTokenJwt;
        }

        private static bool VerificarSenha(string senhaDigitada, byte[] senhaHashBanco)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();

            var hashDigitada = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senhaDigitada));

            return hashDigitada.SequenceEqual(senhaHashBanco);
        }

        public TokenDto Login(LoginDto loginDto)
        {
            Usuario usuario = _repository.ObterPorEmail(loginDto.Email);
            
            if (usuario == null) throw new Exception("Email ou Senha Invalidos.");

            if (usuario.StatusUsuario == false) throw new Exception("Usuario Inativo.");

            if (usuario == null) throw new Exception("Email ou Senha Invalidos.");

            if (!VerificarSenha(loginDto.Senha, usuario.Senha)) throw new Exception("Email ou Senha Invalidos.");

            var token = _tokenJwt.GerarToken(usuario);

            TokenDto novoToken = new TokenDto() { Token = token };

            return novoToken;
        }
    }
}
