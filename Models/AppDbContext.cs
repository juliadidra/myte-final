using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace myte.Models
{
	// Referência ao Banco de Dados - Práticar Herança - Oferecer recursos de integração entre aplicações
	public class AppDbContext : IdentityDbContext<AppUser> 
	{
		//Construtor - método de referência 
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
	}
}
