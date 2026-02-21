using AllinOne.Models;
using ScholarshipPlatform.Data;
using Microsoft.Extensions.DependencyInjection;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Phase 1: Users
        if (!context.Users.Any())
        {
            var ahmed = new User
            {
                Name = "أحمد الطالب",
                Email = "ahmed@student.com",
                UserName = "ahmed@student.com",
                Role = "Student",
                Address = "Cairo, Egypt",
                Age = "25",
                Country = "Egypt" // مهم لتجنب NULL
            };

            var leila = new User
            {
                Name = "ليلى المرشدة",
                Email = "leila@mentor.com",
                UserName = "leila@mentor.com",
                Role = "Mentor",
                Address = "Cairo, Egypt",
                Age = "25",
                Country = "Egypt"
            };

            var mahmoud = new User
            {
                Name = "محمود المدير",
                Email = "admin@platform.com",
                UserName = "admin@platform.com",
                Role = "Admin",
                Address = "Cairo, Egypt",
                Age = "35",
                Country = "Egypt"
            };

            context.Users.AddRange(ahmed, leila, mahmoud);
            context.SaveChanges(); // مهم لتوليد Ids

            // Phase 2: Consultations
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

            // Phase 3: Communities + Chat
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

            // Phase 4: Offices + Scholarships + Applications
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

                // أضف منحة افتراضية
                var scholarship = new Scholarship
                {
                    Title = "منحة ألمانيا 2026",
                    ShortDescription = "منحة لدراسة الماجستير في ألمانيا",
                    Deadline = DateTime.UtcNow.AddMonths(3)
                };
                context.Scholarships.Add(scholarship);

                context.SaveChanges(); // حفظ الكائنات لتوليد Ids

                // أضف طلب المنحة
                context.ScholarshipApplications.Add(new ScholarshipApplication
                {
                    ScholarshipId = scholarship.Id,
                    StudentId = ahmed.Id,
                    OfficeId = office.Id,
                    Status = "Pending",
                    AppliedAt = DateTime.UtcNow
                });
            }

            context.SaveChanges();
        }
    }
}