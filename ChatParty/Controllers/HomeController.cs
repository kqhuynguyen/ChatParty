using System.Diagnostics;
using ChatParty.Areas.Identity.Data;
using ChatParty.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading.Channels;
using System.Linq;

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
            var currentUserId = _userManager.GetUserId(User);
            List<HomeMessageViewModel> homeMessages = new List<HomeMessageViewModel>();

            var channels = await _context.Channel
                .Where(c => c.Users.Any(u => u.Id == currentUserId))
                .OrderByDescending(mg => mg.Messages.OrderByDescending(m=>m.Created).First().Created)
                .Include(mg => mg.Messages.OrderByDescending(m => m.Created).Take(1))
                .ThenInclude(m => m.User)
                .Take(10)
                .ToListAsync();
            foreach (var channel in channels)
            {
                if (channel.Messages.Count > 0)
                {
                    homeMessages.Add(
                        new HomeMessageViewModel
                        {
                            Id = channel.Id,
                            Name = channel.Name,
                            ChatType = ChatType.Channel,
                            LastSender = channel.Messages.ElementAt(0).User.UserName,
                            LastMessage = channel.Messages.ElementAt(0).Content,
                            LastSentAt = channel.Messages.ElementAt(0).Created,
                        }
                    );
                } else
                {
                    homeMessages.Add(
                        new HomeMessageViewModel
                        {
                            Id = channel.Id,
                            Name = channel.Name,
                            ChatType = ChatType.Channel,
                        }
                    );
                }
            }

            var userMessages = await _context.Message
                .Where(m => m.FromId == currentUserId)
                .Include(m => m.From)
                .Include(m => m.To)
                .Select(m => new
                {
                    ReceiverId = m.To.Id,
                    ReceiverName = m.To.UserName,
                    LastSender = m.From.UserName,
                    Content = m.Content,
                    LastSentAt = m.Created
                })
                .Union(
                    _context.Message
                    .Where(m => m.ToId == currentUserId)
                    .Include(m => m.From)
                    .Include(m => m.To)
                    .Select(m => new
                    {
                        ReceiverId = m.From.Id,
                        ReceiverName = m.From.UserName,
                        LastSender = m.From.UserName,
                        Content = m.Content,
                        LastSentAt = m.Created
                    })
                )
                .GroupBy(m =>new { m.ReceiverId })
                .Select(m => m.OrderByDescending(m => m.LastSentAt).First())
                .Take(10)
                .ToListAsync();

            foreach (var userMessage in userMessages)
            {
                homeMessages.Add(
                    new HomeMessageViewModel
                    {
                        Id = userMessage.ReceiverId,
                        Name = userMessage.ReceiverName,
                        ChatType = ChatType.Individual,
                        LastSender = userMessage.LastSender,
                        LastMessage = userMessage.Content,
                        LastSentAt = userMessage.LastSentAt
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