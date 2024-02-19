using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ChatParty.Models
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string? FromId { get; set; }
        public string? ToId { get; set; }
        public User From { get; set; } = null!;
        public User To { get; set; } = null!;
        public string Content { get; set; } = "";
        public DateTime Created { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;
    }
}
