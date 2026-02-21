namespace AllinOne.Models
{
    public class Office
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Website { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ICollection<User>? Agents { get; set; } // Users with Role = Office
        public ICollection<ScholarshipApplication>? Applications { get; set; }
    }
}
