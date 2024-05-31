using Microsoft.AspNetCore.Identity;

namespace myte.Models
{
	// Está classe assume o papel de apresentação (ou entidade (entity) ) da tabela do database.
	// Será praticado o mecanismo de herança com a classe IdentityUser .
	public class AppUser : IdentityUser
	{
	}
}
