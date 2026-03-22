namespace AllinOne.Models
{
    public class ScholarshipRequirement
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public int ScholarshipId { get; set; }
        public Scholarship Scholarship { get; set; } = null!;
    }
}
