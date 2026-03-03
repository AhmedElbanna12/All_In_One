namespace AllinOne.Models
{
    public class Community
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!; // مثلا اسم المنحة أو البلد
        public ICollection<CommunityMember>? Members { get; set; }
        public ICollection<ChatMessage>? Messages { get; set; }
    }
}
