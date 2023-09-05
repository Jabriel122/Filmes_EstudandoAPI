using webapi.filmes.manha.Domains;

namespace webapi.filmes.manha.Interfaces
{
    public interface IUsuarioRepository
    {

        UsuarioDomains Logar(string email, string senha);

    }
}
