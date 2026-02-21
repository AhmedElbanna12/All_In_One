namespace AllinOne.Models
{
    public class Consultation
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public User Student { get; set; } = null!;
        public string MentorId { get; set; }
        public User Mentor { get; set; } = null!;
        public DateTime ScheduledAt { get; set; }
        public string Status { get; set; } = "Pending"; // Pending / Completed / Cancelled
        public string Notes { get; set; } = null!;
}
}
