using Microsoft.AspNetCore.Identity;

namespace PlacesTodo.Models
{
    public class User : IdentityUser
    {
        public int FolderId { get; set; }
        public Folder Folder { get; set; }
    }
}
