namespace Session.Models
{
    public class Sessions
    {

        public int Id { get; set; }
        public string? SessionName { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NbPlace { get; set; }

        public ICollection<Stagiaire>? Stagiaires { get; set; }
        public ICollection<Programme>? Programmes { get; set; }
    }
}
