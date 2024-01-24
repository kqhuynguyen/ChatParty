using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChatParty.Models;

namespace ChatParty.Data
{
    public class ChatPartyContext : DbContext
    {
        public ChatPartyContext (DbContextOptions<ChatPartyContext> options)
            : base(options)
        {
        }

        public DbSet<ChatParty.Models.User> User { get; set; } = default!;
    }
}
