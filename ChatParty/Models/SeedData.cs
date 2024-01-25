using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ChatParty.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using ChatParty.Areas.Identity.Data;

namespace ChatParty.Models;

public static class SeedData
{


    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ChatPartyAuthContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<ChatPartyAuthContext>>()))
        {
            var hasher = new PasswordHasher<User>();

            // Look for any movies.
            if (context.User.Any())
            {
                return;   // DB has been seeded
            }
            context.User.AddRange(
                new User
                {
                    UserName = "Jackson Steward",
                    PasswordHash = hasher.HashPassword(new User(), "abc123456890"),
                    CreatedDate= DateTime.Parse("2023-1-1"),
                    BirthDate = DateTime.Parse("1960-1-1"),
                    Status = 1
                },
                new User
                {
                    UserName = "Yukino Spielberg",
                    PasswordHash = hasher.HashPassword(new User(), "abc123456890"),
                    CreatedDate = DateTime.Parse("2023-9-12"),
                    BirthDate = DateTime.Parse("2001-4-30"),
                    Status = 1
                },
                new User
                {
                    UserName = "Christopher Lennon",
                    PasswordHash = hasher.HashPassword(new User(), "abc123456890"),
                    CreatedDate = DateTime.Parse("2024-1-8"),
                    BirthDate = DateTime.Parse("2005-12-16"),
                    Status = 0
                },
                new User
                {
                    UserName = "Eric Turk",
                    PasswordHash = hasher.HashPassword(new User(), "abc123456890"),
                    CreatedDate = DateTime.Parse("2001-5-8"),
                    BirthDate = DateTime.Parse("1998-9-23"),
                    Status = 1
                }
            );
            context.SaveChanges();
        }
    }
}