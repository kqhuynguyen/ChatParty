using System.ComponentModel.DataAnnotations.Schema;

namespace ChatParty.Models
{
    public class MessageGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
