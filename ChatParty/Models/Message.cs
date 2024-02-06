using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ChatParty.Models
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string MessageGroupId { get; set; }
        public User User { get; set; } = null!;
        public MessageGroup MessageGroup { get; set; } = null!;
        public string Content { get; set; } = "";
        public DateTime Created { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;
    }
}

