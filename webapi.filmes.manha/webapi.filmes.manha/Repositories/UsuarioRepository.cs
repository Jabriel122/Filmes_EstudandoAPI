using System.Data.SqlClient;
using webapi.filmes.manha.Domains;
using webapi.filmes.manha.Interfaces;

namespace webapi.filmes.manha.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        ///<summary>
        /// String de conexão com o banco que recebe os seguintes parâmetros:
        /// Data Source: Nome do Servidor 
        /// Initial Catlog: Nome do banco de dados
        /// Autenticação:
        ///     - Windowns: Integretd Security = true
        ///     - Sql Server: User Id = sa; Pwd = Senha 
        /// </summary>
        private string StringConexao = "Data Source = DESKTOP-VLQ1I1C; Initial Catalog = Filmes_Gabriel; User Id = sa; pwd = Senai@134";


        /// <summary>
        /// Endponit para logar
        /// </summary>
        /// <param name="email"></param>
        /// <param name="senha"></param>
        public UsuarioDomains Logar (string email, string senha)
        {

            using (SqlConnection con = new SqlConnection (StringConexao))
            {
                string querySelect = "SELECT IdUsuario,Email, Permissao FROM Usuario WHERE Email = @Email AND Senha = @Senha";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Senha", senha);

                    SqlDataReader rdr= cmd.ExecuteReader ();

                    if(rdr.Read())
                    {
                        UsuarioDomains usuario = new UsuarioDomains()
                        {
                            IdUsario = Convert.ToInt32(rdr["IdUsuario"]),
                            Email = rdr["Email"].ToString(),
                            Permissao = rdr["Permissao"].ToString()
                        };
                        return usuario;
                    }
                    return null;
                    
                }
            }

        }

    }
}
