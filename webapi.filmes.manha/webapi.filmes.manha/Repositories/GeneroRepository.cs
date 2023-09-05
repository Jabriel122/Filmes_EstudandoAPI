using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using webapi.filmes.manha.Domains;
using webapi.filmes.manha.Interfaces;

namespace webapi.filmes.manha.Repositories
{ 
    public class GeneroRepository : IGeneroRepository
    {
        /// <summary>
        /// String de conexão com o banco que recebe os seguintes parâmetros:
        /// Data Source: Nome do Servidor 
        /// Initial Catlog: Nome do banco de dados
        /// Autenticação:
        ///     - Windowns: Integretd Security = true
        ///     - Sql Server: User Id = sa; Pwd = Senha 
        /// </summary>
        private string StringConexao = "Data Source = DESKTOP-VLQ1I1C; Initial Catalog = Filmes_Gabriel; User Id = sa; pwd = Senai@134";
        public void AtualizarIdCorpo(GeneroDomain genero)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryUpdateByCorpo = "UPDATE Genero SET Nome = @novoNome WHERE IdGenero= @novoId";
                using(SqlCommand cmd = new SqlCommand(queryUpdateByCorpo,con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@novoId", genero.IdGenero);
                    cmd.Parameters.AddWithValue("@novoNome", genero.Nome);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// Endpoint para atualizar generos pela Url
        /// </summary>
        /// <param name="id"></param>
        /// <param name="genero"></param>
        public void AtualizarIdUrl(int id, GeneroDomain genero)
        {
            using (SqlConnection con = new SqlConnection (StringConexao))
            {

                string queryUpdateByUrl = "UPDATE Genero SET Nome = @novoNome WHERE IdGenero = @IdBuscado";
                using (SqlCommand cmd = new SqlCommand(queryUpdateByUrl, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@IdBuscado", id);
                    cmd.Parameters.AddWithValue("@novoNome", genero.Nome);

                    cmd.ExecuteNonQuery();
                }

            }
        }
        

        /// <summary>
        /// Buscar um gênero através do seu Id
        /// </summary>
        /// <param name="id"> Id do genêro a ser Buscado</param>
        /// <returns> Objeto buscado ou null caso não seja</returns>
        public GeneroDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string querySelectById = "SELECT IdGenero, Nome FROM Genero WHERE IdGenero = @IdGenero";

                //abre a conexao com o banco de dados
                con.Open();

                //Declara o SqlCommand rdr para receber os valores do banco de dados
                SqlDataReader rdr;

                //declarar a SqlCommand cmd passando a query que será executada e a conexão parânetros
                using (SqlCommand cmd = new SqlCommand(querySelectById, con)) 
                {
                    //Passa o valor do Parâmetro @Genero
                    cmd.Parameters.AddWithValue("@IdGenero", id);

                    //executar a query e armazenas os dados no rdr
                    rdr = cmd.ExecuteReader();
                   
                    //Verificar se o resultado da query retornou algum registro
                    if(rdr.Read())
                    {
                        GeneroDomain generoBuscado = new GeneroDomain()
                        {
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                            Nome = rdr["Nome"].ToString()
                        };
                        return generoBuscado; 
                    }

                    return null;
                }
            }
        }
        //Metodo Deletar
        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryDelete = "DELETE FROM Genero WHERE IdGenero = @IdGenero ";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    //Passa o valor do Parâmetro @Genero
                    cmd.Parameters.AddWithValue("@IdGenero", id);

                    //abre a conexao com o banco de dados
                    con.Open();

                    //executar a query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        

        /// <summary>
        /// cadastrar um novo genero
        /// </summary>
        /// <param name="novoGenero"> objeto com as informacoes que serao cadastradas <param>
        public void Cadastrar(GeneroDomain novoGenero)
        {

            //declara a conexao passando a string de conexao como parametro 

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //declara a query que sera executada
                string queryInsert = "INSERT INTO Genero (Nome) VALUES (@Nome)" ;

                //declara o sqlcommand com a query que sera executada e a conexao com o bd
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    //Passa o valor do Parâmetr @Nome
                    cmd.Parameters.AddWithValue("@Nome", novoGenero.Nome);

                    //abre a conexao com o banco de dados
                    con.Open();

                    //executar a query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        

        /// <summary>
        /// Listar todos os objetos gêneros
        /// </summary>
        /// <returns> Lista de objetos (gêneros)</returns>
        /// <exception cref="NotImplementedException"></exception>

        public List<GeneroDomain> ListarTodos()
        {
            //Criar uma lista de objetos do tipo gênero
            List<GeneroDomain> listaGeneros= new List<GeneroDomain>();


            //Declarar a SqlCOnnection passando a string de conexõ como parâmetro (Acredito que podemos usar o Sql, pois baixamos o Sql e habilitamos ele durante a programação)
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declarar a instrução a ser executada
                string querySelectAll = "SELECT IdGenero, Nome FROM Genero";
                
                //Abre conexão com banco de dados (Ou seja é uma ponte para o Banco de dados/ A porta se abriu e os dados entraram, agora é permitido, pelo menos por aqui, mecher com os bancos de dados)
                con.Open();

                //Declara o SqlDataReader para percorrer a tabela do Banco de dados (Acredtio que esse código é tipo um carro turista que mostra a tabela pra você e lê ele para você... Não sei, acho que estou errado)
                SqlDataReader rdr;
                
                //Declar SqlCommand passando a query que será executada e a conexão com o bd
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    //executa a query e armazena os dados no rdr 
                    rdr  = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        GeneroDomain genero = new GeneroDomain()
                        {
                            //Atribui a propriedade IdGenero o valor recebido no rdr
                            IdGenero = Convert.ToInt32(rdr[0]),
                            //Atribui a propriedade "Nome" o valor recebido no rdr
                            Nome = rdr["Nome"].ToString() 
                        };
                        //Adciona cada objeto dentro da lista
                        listaGeneros.Add(genero);
                    }
                }
            }

            return listaGeneros;
        }
    }
}
