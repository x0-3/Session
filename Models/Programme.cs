namespace Session.Models
{
    public class Programme
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int ModuleId { get; set; }
        public int Duree { get; set; }

        public Sessions? Session { get; set; }
        public Module? Module { get; set; }
    }
}
