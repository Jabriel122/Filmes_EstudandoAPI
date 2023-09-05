using System.ComponentModel.DataAnnotations;

namespace webapi.filmes.manha.Domains
{
    public class FilmeDomains
    {

        public int IdFilme { get; set; }


        [Required(ErrorMessage = "O titulo de um filme é obrigatório!")]
        public string Titulo { get; set; }

        public int IdGenero { get; set; }   

        public GeneroDomain Genero { get; set; }

    }
}
