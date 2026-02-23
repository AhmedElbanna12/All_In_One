using AllinOne.Models;
using ScholarshipPlatform.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        if (!context.Users.Any())
        {
            // ================= أحمد =================
            var ahmed = new User
            {
                Name = "أحمد الطالب",
                Email = "ahmed@student.com",
                UserName = "ahmed@student.com",
                Role = "Student",
                Address = "Cairo, Egypt",
                Age = "25",
                Country = "Egypt",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(ahmed, "Ahmed@123");

            // ================= ليلى =================
            var leila = new User
            {
                Name = "ليلى المرشدة",
                Email = "leila@mentor.com",
                UserName = "leila@mentor.com",
                Role = "Mentor",
                Address = "Cairo, Egypt",
                Age = "25",
                Country = "Egypt",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(leila, "Leila@123");

            // ================= محمود =================
            var mahmoud = new User
            {
                Name = "محمود المدير",
                Email = "admin@platform.com",
                UserName = "admin@platform.com",
                Role = "Admin",
                Address = "Cairo, Egypt",
                Age = "35",
                Country = "Egypt",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(mahmoud, "Admin@123");

            await context.SaveChangesAsync();
            // ================= باقي البيانات =================

            if (!context.Consultations.Any())
            {
                context.Consultations.Add(new Consultation
                {
                    StudentId = ahmed.Id,
                    MentorId = leila.Id,
                    ScheduledAt = DateTime.UtcNow.AddDays(3),
                    Status = "Pending",
                    Notes = "استشارة عن منح الماجستير"
                });
            }

            if (!context.Communities.Any())
            {
                var community = new Community { Name = "طلاب منحة ألمانيا 2026" };
                context.Communities.Add(community);

                context.ChatMessages.Add(new ChatMessage
                {
                    Community = community,
                    SenderId = ahmed.Id,
                    Message = "مرحبا بالطلاب الجدد!",
                    SentAt = DateTime.UtcNow
                });
            }

            if (!context.Offices.Any())
            {
                var office = new Office
                {
                    Name = "مكتب التقديم الأوروبي",
                    Website = "https://office.com",
                    ContactEmail = "contact@office.com",
                    Description = "مساعدة الطلاب على التقديم للمنح"
                };
                context.Offices.Add(office);

                var scholarship = new Scholarship
                {
                    Title = "منحة ألمانيا 2026",
                    ShortDescription = "منحة لدراسة الماجستير في ألمانيا",
                    Deadline = DateTime.UtcNow.AddMonths(3)
                };
                context.Scholarships.Add(scholarship);

                await context.SaveChangesAsync();

                context.ScholarshipApplications.Add(new ScholarshipApplication
                {
                    ScholarshipId = scholarship.Id,
                    StudentId = ahmed.Id,
                    OfficeId = office.Id,
                    Status = "Pending",
                    AppliedAt = DateTime.UtcNow
                });
            }

            await context.SaveChangesAsync();
        }
    }
}