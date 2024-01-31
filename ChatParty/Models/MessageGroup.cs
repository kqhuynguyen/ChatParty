namespace ChatParty.Models
{
    public class MessageGroup
    {
        public string Id { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
