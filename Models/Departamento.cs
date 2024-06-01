using System.ComponentModel.DataAnnotations;

namespace myte.Models
{
    public class Departamento
    {
        public int? Id { get; set; }
        [Required]
        public string? Nome { get; set; }
    }
}
