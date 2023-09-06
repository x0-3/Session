namespace Session.Models
{
    public class Stagiaire
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Cp { get; set; }
        public string? email { get; set; }
        public int? tel { get; set; }

        public ICollection<Sessions>? Sessions { get; set; }
    }
}
