using Microsoft.AspNetCore.Identity;

namespace ChatParty.Models
{
    public class User : IdentityUser
    {
        public DateTime CreatedDate { get; set; }
        public DateTime BirthDate { get; set; }
        public int Status { get; set; }
        public ICollection<MessageGroup> MessageGroups { get; set; } = new List<MessageGroup>();

    }
}
