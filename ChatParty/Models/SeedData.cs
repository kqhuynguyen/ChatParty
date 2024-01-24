using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ChatParty.Data;
using ChatParty.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace ChatParty.Models;

public static class SeedData
{


    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ChatPartyContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<ChatPartyContext>>()))
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
                    Name = "Jackson Steward",
                    Password = hasher.HashPassword(null, "abc123456890"),
                    CreatedDate= DateTime.Parse("2023-1-1"),
                    BirthDate = DateTime.Parse("1960-1-1"),
                    Status = 1
                },
                new User
                {
                    Name = "Yukino Spielberg",
                    Password = hasher.HashPassword(null, "abc123456890"),
                    CreatedDate = DateTime.Parse("2023-9-12"),
                    BirthDate = DateTime.Parse("2001-4-30"),
                    Status = 1
                },
                new User
                {
                    Name = "Christopher Lennon",
                    Password = hasher.HashPassword(null, "abc123456890"),
                    CreatedDate = DateTime.Parse("2024-1-8"),
                    BirthDate = DateTime.Parse("2005-12-16"),
                    Status = 0
                },
                new User
                {
                    Name = "Eric Turk",
                    Password = hasher.HashPassword(null, "abc123456890"),
                    CreatedDate = DateTime.Parse("2001-5-8"),
                    BirthDate = DateTime.Parse("1998-9-23"),
                    Status = 1
                }
            );
            context.SaveChanges();
        }
    }
}