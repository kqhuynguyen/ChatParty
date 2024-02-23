using System.ComponentModel.DataAnnotations;

namespace ChatParty.Models
{
    public class CreateChannelViewModel
    {
        [
            Required, 
            MaxLength(255)
        ]
        public string Name { get; set; } = "Untitled Channel";
        [
            Required, 
            MinLength(3, ErrorMessage = "Group cant have less than 3 members."), 
            MaxLength(500, ErrorMessage = "Group cant have more than 500 members.")
        ]
        public IEnumerable<string> UserIds { get; set; }
    }
}
