using System.Diagnostics;
using ChatParty.Areas.Identity.Data;
using ChatParty.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> All()
        {
            var channels = await _context.Channel
                .OrderByDescending(mg => mg.Messages.OrderByDescending(m=>m.Created).First().Created)
                .Include(mg => mg.Messages.OrderByDescending(m => m.Created).Take(1))
                .ThenInclude(m => m.User)
                .Take(10)
                .ToListAsync();

            return View(channels);
        }

        [AllowAnonymous]
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