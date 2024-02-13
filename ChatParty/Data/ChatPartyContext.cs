using Microsoft.EntityFrameworkCore;

namespace ChatParty.Data;

public class ChatPartyContext : DbContext
{
    public ChatPartyContext(DbContextOptions<ChatPartyContext> options)
        : base(options)
    {
    }
    public DbSet<Models.User> User { get; set; } = default!;
    public DbSet<Models.Message> Messages { get; set; } = default!;

}
