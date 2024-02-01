using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ChatParty.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using ChatParty.Areas.Identity.Data;
using ChatParty.Data;

namespace ChatParty.Models;

public static class SeedData
{
    private static readonly PasswordHasher<User> hasher = new PasswordHasher<User>();

    private static User createUserPassword(User user, string password)
    {
        user.PasswordHash = hasher.HashPassword(user, password);
        return user;
    }

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ChatPartyAuthContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<ChatPartyAuthContext>>()))
        {



            // Look for any movies.
            if (context.User.Any() || context.User.Any() || context.MessageGroup.Any())
            {
                return;   // DB has been seeded
            }

            var exampleUserId1 = Guid.NewGuid().ToString();
            var exampleUserId2 = Guid.NewGuid().ToString();


            context.User.AddRange(
                createUserPassword(new User
                {
                    Id = exampleUserId1,
                    UserName = "Jackson Steward",
                    Email="jackson@gmail.com",
                    CreatedDate= DateTime.Parse("2023-1-1"),
                    BirthDate = DateTime.Parse("1960-1-1"),
                    Status = 1
                }, "abc123456890"),
                createUserPassword(new User
                {
                    Id = exampleUserId2,
                    UserName = "Yukino Spielberg",
                    Email = "yukino@gmail.com",
                    CreatedDate = DateTime.Parse("2023-9-12"),
                    BirthDate = DateTime.Parse("2001-4-30"),
                    Status = 1
                }, "abc123456890"),
                createUserPassword(new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "Christopher Lennon",
                    Email = "chris@gmail.com",
                    CreatedDate = DateTime.Parse("2024-1-8"),
                    BirthDate = DateTime.Parse("2005-12-16"),
                    Status = 0
                }, "abc123456890"),
                createUserPassword(new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "Eric Turk",
                    Email = "eric@gmail.com",
                    CreatedDate = DateTime.Parse("2001-5-8"),
                    BirthDate = DateTime.Parse("1998-9-23"),
                    Status = 1
                }, "abc123456890")
            );

            context.MessageGroup.AddRange(
                new MessageGroup
                {
                    Id = Constants.PublicMessageGroupId
                }
            );

            context.Message.AddRange(
                new Message
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = exampleUserId1,
                    MessageGroupId = Constants.PublicMessageGroupId,
                    Content = "Hello friends. Jackson's here. How are you today?",
                },
                new Message
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = exampleUserId2,
                    MessageGroupId = Constants.PublicMessageGroupId,
                    Content = "Hi Jackson. I'm doing ok.",
                }
            );

            context.SaveChanges();
        }
    }
}