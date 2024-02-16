using System.Reflection.Metadata;
using ChatParty.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatParty.Areas.Identity.Data;

public class ChatPartyAuthContext : IdentityDbContext<User>
{
    #region Required
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<Message>()
            .HasOne(e => e.From)
            .WithMany()
            .HasForeignKey(e => e.FromId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .Entity<Message>()
            .HasOne(e => e.To)
            .WithMany()
            .HasForeignKey(e => e.ToId)
            .OnDelete(DeleteBehavior.NoAction);
        
    }
    #endregion
    public ChatPartyAuthContext(DbContextOptions<ChatPartyAuthContext> options)
		: base(options)
	{
	}
	public DbSet<Models.User> User { get; set; } = default!;
	public DbSet<Models.GroupMessage> GroupMessage { get; set; } = default!;
    public DbSet<Models.Message> Message { get; set; } = default!;
    public DbSet<Models.Channel> Channel { get; set; } = default!;

}
