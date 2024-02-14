using ChatParty.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatParty.Areas.Identity.Data;

public class ChatPartyAuthContext : IdentityDbContext<User>
{
	public ChatPartyAuthContext(DbContextOptions<ChatPartyAuthContext> options)
		: base(options)
	{
	}
	public DbSet<Models.User> User { get; set; } = default!;
	public DbSet<Models.GroupMessage> GroupMessage { get; set; } = default!;
	public DbSet<Models.Channel> Channel { get; set; } = default!;

}
