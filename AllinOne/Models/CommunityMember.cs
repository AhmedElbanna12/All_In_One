namespace AllinOne.Models
{
    public class CommunityMember
    {
        public int CommunityId { get; set; }
        public Community Community { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}
