using Royal_Games.Contexts;
using Royal_Games.Domains;
using Royal_Games.Interfaces;

namespace Royal_Games.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly Royal_GamesContext _context;

        public UsuarioRepository(Royal_GamesContext context)
        {
            _context = context;
        }
        public List<Usuario> Listar()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario? ObterPorId(int id)
        {
            return _context.Usuarios.Find(id);
        }
        public Usuario? ObterPorEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(usuario => usuario.Email == email);
        }
        public bool EmailExiste(string email)
        {
            return _context.Usuarios.Any(usuario => usuario.Email == email);
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void Atualizar(Usuario usuario)
        {
            Usuario? usuarioBanco = _context.Usuarios.FirstOrDefault(usuarioAux => usuarioAux.UsuarioID == usuario.UsuarioID);

            if (usuarioBanco == null) return;

            usuarioBanco.Nome = usuario.Nome;
            usuarioBanco.Email = usuario.Email;
            usuarioBanco.Senha = usuario.Senha;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Usuario? usuario = _context.Usuarios.FirstOrDefault(usuarioAux => usuarioAux.UsuarioID == id);

            if (usuario == null) return;

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }
    }
}
