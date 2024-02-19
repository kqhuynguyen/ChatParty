using System.Diagnostics;
using ChatParty.Areas.Identity.Data;
using ChatParty.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading.Channels;

namespace ChatParty.Controllers
{

    public class HomeController : Controller
    {
        private readonly ChatPartyAuthContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;


        public HomeController(ChatPartyAuthContext context, ILogger<HomeController> logger, UserManager<User> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> All()
        {
            List<HomeMessage> homeMessages = new List<HomeMessage>();

            var channels = await _context.Channel
                .OrderByDescending(mg => mg.Messages.OrderByDescending(m=>m.Created).First().Created)
                .Include(mg => mg.Messages.OrderByDescending(m => m.Created).Take(1))
                .ThenInclude(m => m.User)
                .Take(10)
                .ToListAsync();
            foreach (var channel in channels)
            {
                homeMessages.Add(
                    new HomeMessage
                    {
                        Id = channel.Id,
                        Name = channel.Name,
                        ChatType = ChatType.Channel,
                        LastSender = channel.Messages.ElementAt(0).User.UserName,
                        LastMessage = channel.Messages.ElementAt(0).Content,
                        LastSentAt = channel.Messages.ElementAt(0).Created,
                    }
                );
            }
            var currentUserId = _userManager.GetUserId(User);
            var userMessages = await _context.Message
                .Where(m => (m.FromId == currentUserId || (m.ToId == currentUserId)))
                .Include(m => m.From)
                .Include(m => m.To)
                .GroupBy(m => m.ToId)
                .Select(m => m.OrderByDescending(m => m.Created).First())
                .Take(10)
                .ToListAsync();
            foreach (var userMessage in userMessages)
            {
                homeMessages.Add(
                    new HomeMessage
                    {
                        Id = userMessage.ToId != currentUserId ? userMessage.ToId : userMessage.FromId,
                        Name = userMessage.ToId != currentUserId ? userMessage.To.UserName : userMessage.From.UserName,
                        ChatType = ChatType.Individual,
                        LastSender = userMessage.From.UserName,
                        LastMessage = userMessage.Content,
                        LastSentAt = userMessage.Created
                    }
                );
            }
            homeMessages = homeMessages.OrderByDescending(x => x.LastSentAt).ToList();
            return View(homeMessages);
        }

        public IActionResult Index()
        {
            return Redirect("/Home/All");
        }

        [AllowAnonymous]
        public IActionResult Privacy()

        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}