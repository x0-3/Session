using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Session.Models
{
    public class Module
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
        public ICollection<Programme>? Programmes { get; set; }

    }
}
