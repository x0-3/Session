namespace Session.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Module>? Modules { get; set; }
    }
}
