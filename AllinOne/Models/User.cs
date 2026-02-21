using Microsoft.AspNetCore.Identity;

namespace AllinOne.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = null!;
        public string Age { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Role { get; set; } = "Student"; // Admin / Student / Mentor / Office
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<UserScholarship>? UserScholarships { get; set; }
        public ICollection<Consultation>? StudentConsultations { get; set; } // if Role=Student
        public ICollection<Consultation>? MentorConsultations { get; set; } // if Role=Mentor
        public ICollection<ChatMessage>? ChatMessages { get; set; }

        public ICollection<Community>? Communities { get; set; }
    }
}
