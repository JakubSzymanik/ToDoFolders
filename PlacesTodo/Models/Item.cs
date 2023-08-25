using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlacesTodo.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public Folder? Parent { get; set; }

        [DisplayName("Name")]
        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }

        [MinLength(2), MaxLength(20)]
        [DisplayName("Description")]
        public string? Description { get; set; }

        [DisplayFormat(DataFormatString = "{dd/mm/yyyy}")]
        [DisplayName("Due")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
    }
}
