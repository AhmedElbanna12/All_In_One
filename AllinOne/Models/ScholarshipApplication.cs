namespace AllinOne.Models
{
    public class ScholarshipApplication
    {
        public int Id { get; set; }
        public int ScholarshipId { get; set; }
        public Scholarship Scholarship { get; set; } = null!;
        public string StudentId { get; set; }
        public User Student { get; set; } = null!;
        public int OfficeId { get; set; }
        public Office Office { get; set; } = null!;
        public string Status { get; set; } = "Pending"; // Pending / Submitted / Accepted / Rejected
        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
    }
}
