namespace myte.Models
{
    public class CalendarViewModel
    {
        public int Day { get; set; }
        public string DayOfWeek { get; set; }

        public string WbsCodigo { get; set; }
        public double Hours { get; set; }
        public DateTime Date { get; set; } // Adicione essa linha
    }
}