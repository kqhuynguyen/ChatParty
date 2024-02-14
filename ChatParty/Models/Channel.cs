using System.ComponentModel.DataAnnotations.Schema;

namespace ChatParty.Models
{
    public class Channel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<GroupMessage> Messages { get; set; } = new List<GroupMessage>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
