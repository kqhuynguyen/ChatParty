using ChatParty.Areas.Identity.Data;
using ChatParty.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatParty.Models;

public static class SeedData
{
    private static readonly PasswordHasher<User> hasher = new PasswordHasher<User>();

    public async static Task Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ChatPartyAuthContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<ChatPartyAuthContext>>()))
        {


            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            // Look for any movies.
            if (context.User.Any() || context.User.Any() || context.MessageGroup.Any())
            {
                return;   // DB has been seeded
            }

            var exampleUserId1 = Guid.NewGuid().ToString();
            var exampleUserId2 = Guid.NewGuid().ToString();

            var result = await userManager.CreateAsync(new User
            {
                Id = exampleUserId1,
                UserName = "JacksonSteward",
                Email = "jackson@gmail.com",
                CreatedDate = DateTime.Parse("2023-1-1"),
                EmailConfirmed = true,
                BirthDate = DateTime.Parse("1960-1-1"),
                Status = 1
            }, "abc123456890");
            await userManager.CreateAsync(new User
            {
                Id = exampleUserId2,
                UserName = "YukinoSpielberg",
                Email = "yukino@gmail.com",
                CreatedDate = DateTime.Parse("2023-9-12"),
                EmailConfirmed = true,
                BirthDate = DateTime.Parse("2001-4-30"),
                Status = 1
            }, "abc123456890");
            await userManager.CreateAsync(new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "ChristopherLennon",
                Email = "chris@gmail.com",
                CreatedDate = DateTime.Parse("2024-1-8"),
                EmailConfirmed = true,
                BirthDate = DateTime.Parse("2005-12-16"),
                Status = 0
            }, "abc123456890");
            await userManager.CreateAsync(new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "EricTurk",
                Email = "eric@gmail.com",
                CreatedDate = DateTime.Parse("2001-5-8"),
                EmailConfirmed = true,
                BirthDate = DateTime.Parse("1998-9-23"),
                Status = 1
            }, "abc123456890");

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