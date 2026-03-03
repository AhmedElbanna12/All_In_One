using AllinOne.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ScholarshipPlatform.Data
{
    public class ApplicationDbContext : IdentityDbContext<User> // User يرث IdentityUser
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // Phase 1
        public DbSet<Scholarship> Scholarships => Set<Scholarship>();
        public DbSet<UserScholarship> UserScholarships => Set<UserScholarship>();

        // Phase 2
        public DbSet<Consultation> Consultations => Set<Consultation>();

        // Phase 3
        public DbSet<Community> Communities => Set<Community>();
        public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();

        // Phase 4
        public DbSet<Office> Offices => Set<Office>();
        public DbSet<ScholarshipApplication> ScholarshipApplications => Set<ScholarshipApplication>();


        public DbSet<CommunityMember> CommunityMembers => Set<CommunityMember>();


        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // UserScholarship Many-to-Many
            modelBuilder.Entity<UserScholarship>()
                .HasKey(us => new { us.UserId, us.ScholarshipId });

            modelBuilder.Entity<UserScholarship>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserScholarships)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<UserScholarship>()
                .HasOne(us => us.Scholarship)
                .WithMany(s => s.UserScholarships)
                .HasForeignKey(us => us.ScholarshipId);

            modelBuilder.Entity<CommunityMember>()
     .HasKey(cm => new { cm.CommunityId, cm.UserId });

            modelBuilder.Entity<CommunityMember>()
                .HasOne(cm => cm.Community)
                .WithMany(c => c.Members)
                .HasForeignKey(cm => cm.CommunityId);

            modelBuilder.Entity<CommunityMember>()
                .HasOne(cm => cm.User)
                .WithMany(u => u.Communities)
                .HasForeignKey(cm => cm.UserId);

            // ChatMessage Relations
            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.Community)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.CommunityId);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.ChatMessages)
                .HasForeignKey(m => m.SenderId);

            // Consultation Relations
            modelBuilder.Entity<Consultation>()
                .HasOne(c => c.Student)
                .WithMany(u => u.StudentConsultations)
                .HasForeignKey(c => c.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Consultation>()
                .HasOne(c => c.Mentor)
                .WithMany(u => u.MentorConsultations)
                .HasForeignKey(c => c.MentorId)
                .OnDelete(DeleteBehavior.Restrict);

            // ScholarshipApplication Relations
            modelBuilder.Entity<ScholarshipApplication>()
                .HasOne(sa => sa.Scholarship)
                .WithMany()
                .HasForeignKey(sa => sa.ScholarshipId);

            modelBuilder.Entity<ScholarshipApplication>()
                .HasOne(sa => sa.Student)
                .WithMany()
                .HasForeignKey(sa => sa.StudentId);

            modelBuilder.Entity<ScholarshipApplication>()
                .HasOne(sa => sa.Office)
                .WithMany(o => o.Applications)
                .HasForeignKey(sa => sa.OfficeId);
        }
    }
}