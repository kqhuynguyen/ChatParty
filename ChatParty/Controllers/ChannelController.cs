using ChatParty.Areas.Identity.Data;
using ChatParty.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatParty.Controllers
{
    public class ChannelController : Controller
    {
        private readonly ChatPartyAuthContext _context;

        public ChannelController(ChatPartyAuthContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? id)
        {
            if (id == null || _context.Channel == null)
            {
                return NotFound();
            }
            var messageGroup = await _context.Channel.Include(
                    mg => mg.Messages.OrderBy(m => m.Created)
                ).ThenInclude(m => m.User)
                .Where(mg => mg.Id == id)
                .FirstOrDefaultAsync();
            if (messageGroup == null)
            {
                return NotFound();
            }
            return View(messageGroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name", "Users")] Channel channel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(channel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = channel.Id } );
            }
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(string channelId, string userId)
        {
            if (ModelState.IsValid)
            {
                var channel = await _context.Channel.Include(c => c.Users).Where(c => c.Id == channelId).FirstOrDefaultAsync();
                if (channel == null)
                {
                    return NotFound();
                }
                var user = await _context.User.Where(u => u.Id == userId).FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound();
                }
                channel.Users.Add(user);
                await _context.SaveChangesAsync();
                return Ok(user.Id);
            }
            return BadRequest();
        }
    }
}
