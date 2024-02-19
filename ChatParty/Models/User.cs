using Microsoft.AspNetCore.Identity;

namespace ChatParty.Models
{
    public class User : IdentityUser
    {
        public DateTime CreatedDate { get; set; }
        public DateTime BirthDate { get; set; }
        public int Status { get; set; }
        public ICollection<Channel> Channel { get; set; } = new List<Channel>();

    }
}
