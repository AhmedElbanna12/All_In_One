namespace AllinOne.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int CommunityId { get; set; }
        public Community Community { get; set; } = null!;
        public string SenderId { get; set; }
        public User Sender { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
