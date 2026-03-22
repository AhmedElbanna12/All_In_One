namespace AllinOne.Models
{
    public class ScholarshipMajor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!; // هندسة - طب - إدارة

        public int ScholarshipId { get; set; }
        public Scholarship Scholarship { get; set; } = null!;
    }
}
