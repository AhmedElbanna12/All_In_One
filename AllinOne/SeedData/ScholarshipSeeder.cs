using AllinOne.Models;
using ScholarshipPlatform.Data;

namespace AllinOne.SeedData
{
    public static class ScholarshipSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Scholarships.Any()) return;

            var scholarship = new Scholarship
            {
                Title = "المنحة التركية",
                Slug = "turkiye-burslari",
                Country = "تركيا",
                FundingType = "تمويل كامل",
                DegreeLevel = "بكالوريوس / ماجستير / دكتوراه",
                Deadline = new DateTime(2026, 2, 20),
                ShortDescription = "منحة حكومية تركية ممولة بالكامل تشمل الدراسة، السكن، التأمين، وراتب شهري.",
                FullDescription = "تعد المنحة التركية واحدة من أقوى المنح العالمية التي تقدمها الحكومة التركية للطلاب الدوليين، وتشمل جميع التكاليف الدراسية والمعيشية.",
                OfficialLink = "https://www.turkiyeburslari.gov.tr/",
                Featured = true
            };

            context.Scholarships.Add(scholarship);
            context.SaveChanges();


            var majors = new List<ScholarshipMajor>
        {
            new() { Name = "الطب", Category = "طبية", ScholarshipId = scholarship.Id },
            new() { Name = "الصيدلة", Category = "طبية", ScholarshipId = scholarship.Id },
            new() { Name = "الهندسة المدنية", Category = "هندسة", ScholarshipId = scholarship.Id },
            new() { Name = "هندسة البرمجيات", Category = "هندسة", ScholarshipId = scholarship.Id },
            new() { Name = "إدارة الأعمال", Category = "إدارة", ScholarshipId = scholarship.Id },
            new() { Name = "الاقتصاد", Category = "إدارة", ScholarshipId = scholarship.Id },
            new() { Name = "العلوم السياسية", Category = "علوم إنسانية", ScholarshipId = scholarship.Id },
            new() { Name = "علم النفس", Category = "علوم إنسانية", ScholarshipId = scholarship.Id }
        };

            context.ScholarshipMajors.AddRange(majors);



            var docs = new List<RequiredDocument>
        {
            new() { Title = "جواز السفر", Description = "وثيقة هوية سارية", ScholarshipId = scholarship.Id },
            new() { Title = "صورة شخصية", Description = "حديثة خلال سنة", ScholarshipId = scholarship.Id },
            new() { Title = "شهادة التخرج", Description = "أو إثبات قيد", ScholarshipId = scholarship.Id },
            new() { Title = "بيان الدرجات", Description = "Transcript", ScholarshipId = scholarship.Id },
            new() { Title = "شهادة لغة", Description = "TOEFL أو DELF", ScholarshipId = scholarship.Id },
            new() { Title = "اختبارات دولية", Description = "SAT / GRE / GMAT", ScholarshipId = scholarship.Id }
        };

            context.RequiredDocuments.AddRange(docs);



            var benefits = new List<ScholarshipBenefit>
        {
            new() { Description = "تغطية كاملة للرسوم الدراسية", ScholarshipId = scholarship.Id },
            new() { Description = "راتب شهري", ScholarshipId = scholarship.Id },
            new() { Description = "سكن مجاني", ScholarshipId = scholarship.Id },
            new() { Description = "تأمين صحي", ScholarshipId = scholarship.Id },
            new() { Description = "تذاكر طيران", ScholarshipId = scholarship.Id },
            new() { Description = "سنة لغة تركية مجانية", ScholarshipId = scholarship.Id }
        };

            context.ScholarshipBenefits.AddRange(benefits);



            var requirements = new List<ScholarshipRequirement>
        {
            new() { Description = "ألا يزيد عمر البكالوريوس عن 21 سنة", ScholarshipId = scholarship.Id },
            new() { Description = "ألا يزيد عمر الماجستير عن 30 سنة", ScholarshipId = scholarship.Id },
            new() { Description = "معدل لا يقل عن 75%", ScholarshipId = scholarship.Id }
        };

            context.ScholarshipRequirements.AddRange(requirements);



            var stipends = new List<ScholarshipStipend>
        {
            new() { DegreeLevel = "بكالوريوس", Amount = 6500, ScholarshipId = scholarship.Id },
            new() { DegreeLevel = "ماجستير", Amount = 9500, ScholarshipId = scholarship.Id },
            new() { DegreeLevel = "دكتوراه", Amount = 13000, ScholarshipId = scholarship.Id }
        };

            context.ScholarshipStipends.AddRange(stipends);

            context.SaveChanges();
        }
    }
}
    