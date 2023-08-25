using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlacesTodo.Models;

namespace PlacesTodo.Context
{
    public class AppDBContext : IdentityDbContext<User>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        { }

        public DbSet<Folder> Folders { get; set; }
        public DbSet<Item> Tasks { get; set; }
    }
}
