using System.ComponentModel.DataAnnotations;

namespace myte.Models
{
	//Model que vai lidar com o contexto de Validação e Autenticação.
	//******* Esta classe assume o papel de Model do Main da aplicação. ***********
	public class User
	{
     
        [Key]
		[RegularExpression("[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9])+\\.[a-zA-Z]{2,6}$")]
		public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
	}
}
