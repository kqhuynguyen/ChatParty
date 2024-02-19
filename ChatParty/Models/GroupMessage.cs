using System.ComponentModel.DataAnnotations.Schema;

namespace ChatParty.Models
{
    public class GroupMessage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ChannelId { get; set; }
        public User User { get; set; } = null!;
        public Channel MessageGroup { get; set; } = null!;
        public string Content { get; set; } = "";
        public DateTime Created { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;
    }
}

