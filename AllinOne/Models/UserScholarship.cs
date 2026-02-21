namespace AllinOne.Models
{
    public class UserScholarship
    {
        public string UserId { get; set; }
        public User User { get; set; } = null!;

        public int ScholarshipId { get; set; }
        public Scholarship Scholarship { get; set; } = null!;
    }
}
