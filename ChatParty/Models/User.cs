using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ChatParty.Models
{
    public class User: IdentityUser<int>
    {

        public DateTime CreatedDate { get; set; }
        public DateTime BirthDate { get; set; }
        public int Status { get; set; }

    }
}
 