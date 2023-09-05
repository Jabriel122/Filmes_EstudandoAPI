using webapi.filmes.manha.Domains;
using webapi.filmes.manha.Repositories;

namespace webapi.filmes.manha.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório GeneroRepository
    /// Definir os métodos que serão implementados pelo o GeneroRepository
    /// </summary>
    public interface IGeneroRepository
    {
        //tipoRetoretor NomeMetdo(tipoParâmetro NomeParâmentro 
        List<GeneroDomain> ListarTodos();

        /// <summary>
        /// Atulizar objetos existentes passando os seus Id pelo o corpo de Requisão
        /// </summary>
        /// <param name="genero">Objetivo atualizar (Novas informações)</param>
        /// <param name="Id">Ido do objetoque será atualizado</param>
        void AtualizarIdCorpo(GeneroDomain genero);

        /// <summary>
        /// Atulizar objetos existentes passando os seus Id para URL
        /// </summary>
        /// <param name="genero">Objetivo atualizar (Novas informações)</param>
        /// <param name="Id">Ido do objetoque será atualizado</param>
        void AtualizarIdUrl(int id, GeneroDomain genero);


        /// <summary>
        /// Deletar um objeto
        /// </summary>
        /// <param name="id"> Um objeto que será deletad </param>
        void Deletar(int id);


        /// <summary>
        /// Buscar um objeto draes=z
        /// </summary>
        /// <param name="Id">Id do objeto a ser buscadoparam>
        /// <returns> Objeto buscado</returns>
        GeneroDomain BuscarPorId(int id);

        /// <summary>
        /// Listar todos os objetos
        /// </summary>
        /// <returns novo="novoGenero"> objeto atualizado (Novas informaççao)</returns>
        void Cadastrar(GeneroDomain novoGenero);
    }
        
}
