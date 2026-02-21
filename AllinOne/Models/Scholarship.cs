namespace AllinOne.Models
{
    public class Scholarship
    {
        public int Id { get; set; }
        public string Slug { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string FundingType { get; set; } = null!;
        public string DegreeLevel { get; set; } = null!;
        public DateTime Deadline { get; set; }
        public string ShortDescription { get; set; } = null!;
        public string FullDescription { get; set; } = null!;
        public string Eligibility { get; set; } = null!; // JSON
        public string RequiredDocuments { get; set; } = null!; // JSON
        public string ApplicationSteps { get; set; } = null!; // JSON
        public string OfficialLink { get; set; } = null!;
        public bool Featured { get; set; } = false;

        public ICollection<UserScholarship>? UserScholarships { get; set; }
    }
}
