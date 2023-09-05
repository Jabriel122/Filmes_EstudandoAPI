using System.ComponentModel.DataAnnotations;

namespace webapi.filmes.manha.Domains
{
    public class UsuarioDomains
    {

        public int IdUsario { get; set; }

        [Required(ErrorMessage = "Um email é Obrigatório")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Uma senha é obrigatório")]
        public string Senha { get; set; }

        public string Permissao { get; set; }

    }
}
