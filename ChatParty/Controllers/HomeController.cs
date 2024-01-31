using System.Diagnostics;
using ChatParty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChatParty.Areas.Identity.Data;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;

namespace ChatParty.Controllers
{
    public class HomeController : Controller
    {
        private readonly ChatPartyAuthContext _context;
        private readonly ILogger<HomeController> _logger;


        public HomeController(ChatPartyAuthContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var homeMessages = _context.Message
                .Include(message => message.User)
                .OrderBy(message => message.Created)
                .ToList();
            return View(homeMessages.ToList());
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