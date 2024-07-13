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
        using (var context = new ChatPartyContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<ChatPartyContext>>()))
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            if (context.User.Any() || context.User.Any() || context.Channel.Any())
            {
                return;  
            }

            await userManager.CreateAsync(new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "JacksonSteward",
                Email = "jackson@gmail.com",
                CreatedDate = DateTime.Parse("2023-1-1"),
                EmailConfirmed = true,
                BirthDate = DateTime.Parse("1960-1-1"),
                Status = 1
            }, "abc123456890");
            await userManager.CreateAsync(new User
            {
                Id = Guid.NewGuid().ToString(),
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

            context.Channel.AddRange(
                new Channel
                {
                    Id = Constants.PublicMessageGroupId,
                    Name = "Public Group",
                }
            );

            context.SaveChanges();
        }
    }
}