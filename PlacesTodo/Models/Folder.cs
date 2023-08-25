using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlacesTodo.Models
{
    public class Folder
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }
        public Folder? Parent { get; set; }

        public List<Folder>? Children { get; set; }
        public List<Item>? Tasks { get; set; }

        [DisplayName("Name")]
        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }
    }
}
