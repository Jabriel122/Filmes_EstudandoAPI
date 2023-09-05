using webapi.filmes.manha.Domains;

namespace webapi.filmes.manha.Interfaces
{
    public interface IFilmeRepository
    {
        void Cadastre(FilmeDomains filmeNovo);

        List<FilmeDomains> ListarTodos();

        void AtualizarIdCorpo(FilmeDomains filme);

        void AtulaizarIdUrl(int id, FilmeDomains filme);

        void Deletar(int id);

        FilmeDomains BuscarPorId(int id);

    }
}
