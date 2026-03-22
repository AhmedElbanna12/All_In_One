namespace AllinOne.Models
{
    public class RequiredDocument
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public int ScholarshipId { get; set; }
        public Scholarship Scholarship { get; set; } = null!;
    }
}
