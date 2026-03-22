namespace AllinOne.Models
{
    public class ScholarshipStipend
    {
        public int Id { get; set; }
        public string DegreeLevel { get; set; } = null!;
        public decimal Amount { get; set; }

        public int ScholarshipId { get; set; }
        public Scholarship Scholarship { get; set; } = null!;
    }
}
