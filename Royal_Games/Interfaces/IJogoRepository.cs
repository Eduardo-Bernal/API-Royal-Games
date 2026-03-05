using Royal_Games.Domains;

namespace Royal_Games.Interfaces
{
    public interface IJogoRepository
    {
        List<Jogo> Listar();

        Jogo ObterPorID(int id);

        Byte[] ObterImagem(int id);
        bool NomeExiste(string nome, int? JogoIdAtual = null);

        void Cadastrar(Jogo jogo, List<int> generoId);
        void Atualizar(Jogo jogo, List<int> generoId);
        void Remover(int id);
    }
}
