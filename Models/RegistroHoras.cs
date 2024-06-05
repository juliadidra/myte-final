namespace myte.Models
{
    public class RegistroHoras
    {
        public int Id { get; set; }

        public int Horas { get; set; }
        public DateOnly Dia { get; set; }


        public string? Funcionario_Email { get; set; }

        //public Funcionario? Funcionario { get; set; }


        public string? WBS_Codigo { get; set; }
    }

}
