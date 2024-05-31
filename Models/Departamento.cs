using System.ComponentModel.DataAnnotations;

namespace myte.Models
{
    public class Departamento
    {
        [Required]
        public string? Nome { get; set; }
    }
}
