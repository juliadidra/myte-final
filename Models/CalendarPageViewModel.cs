namespace myte.Models
{
    public class CalendarPageViewModel
    {
        public List<CalendarViewModel> CalendarDays { get; set; }
        public List<Wbs> WbsList { get; set; }

        public List<RegistroHoras> RegistroHorasList { get; set; }

        public int Quinzena { get; set; }  // Adicione esta linha
    }
}
