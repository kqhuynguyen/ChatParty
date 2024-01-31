
using ChatParty.Areas.Identity.Data;
using ChatParty.Data;
using ChatParty.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace ChatParty.Hubs
{
    [AllowAnonymous]
    public class ChatHub: Hub
    {
        private readonly ChatPartyAuthContext _authContext;
        private readonly UserManager<User> _userManager;

        public ChatHub(ChatPartyAuthContext authContext, UserManager<User> userManager)
        {
            _authContext = authContext;
            _userManager = userManager;
        }

        public async Task SendMessage(string message)
        {
            var user = await _userManager.GetUserAsync(Context.User);
            var nameOfSender = "Guest";
            if (user != null)
            {
                var messageObject = new Message
                {
                    UserId = user.Id,
                    Content = message,
                    MessageGroupId = Constants.PublicMessageGroupId,
                };
                _authContext.Add(messageObject);
                _authContext.SaveChanges();
            } 

            await Clients.All.SendAsync("ReceiveMessage", nameOfSender, message);
        }
    }
}
