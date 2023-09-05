using System.Data.SqlClient;
using webapi.filmes.manha.Domains;
using webapi.filmes.manha.Interfaces;

namespace webapi.filmes.manha.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {

        private string StringConexao = "Data Source = DESKTOP-VLQ1I1C; Initial Catalog = Filmes_Gabriel; User Id = sa; pwd = Senai@134";
        public void AtualizarIdCorpo(FilmeDomains filme)
        {
           using (SqlConnection con = new SqlConnection(StringConexao))
            {

                string queryUpdateByCorpo = "UPDATE Filme SET Titulo = @novoTitulo, IdGenero = @IdGenero WHERE IdFilme = @novoId";
                using (SqlCommand cmd = new SqlCommand(queryUpdateByCorpo,con))
                {

                    con.Open();
                    cmd.Parameters.AddWithValue("@novoId", filme.IdFilme);
                    cmd.Parameters.AddWithValue("@novoTitulo", filme.Titulo);
                    cmd.Parameters.AddWithValue("@IdGenero", filme.IdGenero);

                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void AtulaizarIdUrl(int id, FilmeDomains filme)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {

                string queryUpdataByUrl = "UPDATE Filme SET Titulo = (@novoTitulo), IdGenero = (@IdGenero) WHERE IdFilme = @IdBuscado";
                using(SqlCommand cmd = new SqlCommand(queryUpdataByUrl,con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@IdBuscado", id );
                    cmd.Parameters.AddWithValue("@novoTitulo", filme.Titulo);
                    cmd.Parameters.AddWithValue("@IdGenero", filme.IdGenero);

                    cmd.ExecuteNonQuery();

                }
                
            }
        }

        public FilmeDomains BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string querySelectById = "SELECT IdFilme, Filme.IdGenero, Titulo,Genero.Nome FROM Filme INNER JOIN Genero ON Genero.IdGenero = Filme.IdGenero WHERE IdFilme = @IdFilme";

                con.Open();

                SqlDataReader rdr;

                using(SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("IdFilme", id);

                    rdr = cmd.ExecuteReader();

                    if(rdr.Read())
                    {
                        FilmeDomains filmeBuscado = new FilmeDomains()
                        {
                            IdFilme = Convert.ToInt32(rdr["IdFilme"]),
                            Titulo = rdr["Titulo"].ToString(),
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                            Genero = new GeneroDomain()
                            {
                                Nome = rdr["Nome"].ToString()
                            } 
                        };
                        return filmeBuscado;
                    }

                    return null;
                }
            }
        }

        public void Cadastre(FilmeDomains filmeNovo)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryInsert = "INSERT INTO Filme (IdGenero, Titulo) VALUES (@IdGenero ,@Titulo)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {

                    con.Open();

                    cmd.Parameters.AddWithValue("@Titulo", filmeNovo.Titulo);
                    cmd.Parameters.AddWithValue("@IdGenero", filmeNovo.IdGenero);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryDelete = "DELETE FROM Filme WHERE IdFilme = @IdFilme";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@IdFilme", id);

                    con.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        List<FilmeDomains> IFilmeRepository.ListarTodos()
        {
            List<FilmeDomains> listarFilmes = new List<FilmeDomains>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string querySelectAll = "SELECT IdFilme, Filme.IdGenero, Titulo,Genero.Nome FROM Filme INNER JOIN Genero ON Genero.IdGenero = Filme.IdGenero";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        FilmeDomains filme = new FilmeDomains()
                        {
                            IdFilme = Convert.ToInt32(rdr["IdFilme"]),
                            Titulo = Convert.ToString(rdr["Titulo"]),
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),

                            Genero = new GeneroDomain()
                            {
                                Nome = rdr["Nome"].ToString()
                            }
                        };
                        listarFilmes.Add(filme);
                    }
                }
            }

            return listarFilmes;
        }
    }
}
