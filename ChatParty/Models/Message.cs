namespace ChatParty.Models
{
    public class Message
    { 
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MessageGroupId { get; set; }
        public User User { get; set; } = null!;
        public MessageGroup MessageGroup { get; set; } = null!;
        public string Content { get; set; } = "";
        public DateTime Created { get; set; }
        public bool Deleted { get; set; }
    }
}

