using Microsoft.EntityFrameworkCore;
using PlacesTodo.Context;

namespace PlacesTodo.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDBContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AppDBContext>>()))
            {
                // Look for any movies.
                if (context.Folders.Any())
                {
                    return;   // DB has been seeded
                }
                context.Folders.AddRange(
                    new Folder() { Name = "Root"}, //1
                    new Folder() { Name = "Math", ParentId = 1}, //2
                    new Folder() { Name = "Biology", ParentId = 1}, //3
                    new Folder() { Name = "Pitagoras", ParentId = 2}); //4

                context.Tasks.AddRange(
                    new Item() { Name = "Pitagoras", Description = "Ogarnąć Pitagorasa", ParentId = 4, DueDate = new DateTime(2023, 10, 2)});
                
                context.SaveChanges();

            }
        }
    }
}
