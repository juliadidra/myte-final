namespace myte.Models
{
    public class Repository
    {
        private static List<Wbs> _todasAsWbs = new List<Wbs>();

        public static IEnumerable<Wbs> TodasAsWbs
        {
            get { return _todasAsWbs; }
        }

        public static void Inserir(Wbs registroWbs)
        {
            _todasAsWbs.Add(registroWbs);
        }

        public static void Excluir(Wbs registroWbs)
        {
            _todasAsWbs.Remove(registroWbs);
        }

       
    }
}
