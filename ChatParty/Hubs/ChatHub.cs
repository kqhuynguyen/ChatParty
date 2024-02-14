
using ChatParty.Areas.Identity.Data;
using ChatParty.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace ChatParty.Hubs
{
    [AllowAnonymous]
    public class ChatHub : Hub
    {
        private readonly ChatPartyAuthContext _authContext;
        private readonly UserManager<User> _userManager;

        public ChatHub(ChatPartyAuthContext authContext, UserManager<User> userManager)
        {
            _authContext = authContext;
            _userManager = userManager;
        }
        public async Task AddToGroup(string groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }
        public async Task SendMessage(string message, string groupId)
        {
            var nameOfSender = "Guest";
            var user = await _userManager.GetUserAsync(Context.User);
            if (user != null)
            {
                var messageObject = new GroupMessage
                {
                    UserId = user.Id,
                    Content = message,
                    ChannelId = groupId,
                };
                nameOfSender = user.UserName;
                _authContext.Add(messageObject);
                _authContext.SaveChanges();
            }

            await Clients.Group(groupId).SendAsync("ReceiveMessage", nameOfSender, message);
        }
    }
}
