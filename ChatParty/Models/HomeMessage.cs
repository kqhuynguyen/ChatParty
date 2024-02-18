namespace ChatParty.Models
{
    public enum ChatType
    {
        Channel,
        Individual
    }
    public class HomeMessage
    {
        public string Id { get; set; }
        public ChatType ChatType { get; set; }
        public string Name { get; set; }
        public string? LastSender { get; set; } = null;
        public string? LastMessage { get; set; } = null;
        public DateTime LastSentAt { get; set; }

    }
}
