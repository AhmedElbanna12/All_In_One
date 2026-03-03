using AllinOne.Models;
using ScholarshipPlatform.Data;

namespace AllinOne.SeedData
{
    public class SeedCommunity
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            // تأكد من إنشاء DB
            context.Database.EnsureCreated();

            // ===== Communities =====
            if (!context.Communities.Any())
            {
                var communities = new List<Community>
            {
                new Community { Name = "المنحه التركيه" },
            };

                context.Communities.AddRange(communities);
                await context.SaveChangesAsync();
            }
        }
    }
}

