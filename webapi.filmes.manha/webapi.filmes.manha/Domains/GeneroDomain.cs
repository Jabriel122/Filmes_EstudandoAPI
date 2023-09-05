using System.ComponentModel.DataAnnotations;

namespace webapi.filmes.manha.Domains
{
    /// <summary>
    /// Classe que representa a entidade Genero
    /// </summary>
    public class GeneroDomain
    {
        public int IdGenero { get; set; }

        [Required(ErrorMessage ="O nome gênero é obrigatório!")]
        public string Nome { get; set; }
    }
}
