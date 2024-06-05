using System.ComponentModel.DataAnnotations;

namespace myte.Models
{
    public class Wbs
    {
        [Required]
        public string? Nome { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z0-9]{4,10}$")]
        //expressao que indica que o codigo deve ter apenas letras e numeros
        //e deve possuir entre 4 e 10 caracteres
        public string? Codigo { get; set; }
        [Required]
        public string? Descricao { get; set; }
        [Required]
        public string? Tipo { get; set; }
        public DateOnly DataCriacao { get; set; }
    }
}
