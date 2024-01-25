﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatParty.Data;

public class ChatPartyContext : DbContext
{
    public ChatPartyContext(DbContextOptions<ChatPartyContext> options)
        : base(options)
    {
    }
    public DbSet<Models.User> User { get; set; } = default!;

}
