using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatParty.Areas.Identity.Data;

public class ChatPartyAuthContext : IdentityDbContext
{
    public ChatPartyAuthContext(DbContextOptions<ChatPartyAuthContext> options)
        : base(options)
    {
    }
    public DbSet<Models.User> User { get; set; } = default!;

}
