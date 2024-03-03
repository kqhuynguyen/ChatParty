using ChatParty.Areas.Identity.Data;
using ChatParty.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatParty.Controllers
{
    public class ChannelController : Controller
    {
        private readonly ChatPartyAuthContext _context;
        private readonly UserManager<User> _userManager;

        public ChannelController(ChatPartyAuthContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody, Bind("Name", "UserIds")] CreateChannelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var channel = new Channel { Name = viewModel.Name };
                _context.Add(channel);
                foreach (var userId in viewModel.UserIds)
                { 
                    var user = await _context.User.Where(u => u.Id == userId).FirstOrDefaultAsync();
                    if (user == null)
                    {
                        return NotFound("User not found: " + userId);
                    }
                    channel.Users.Add(user);
                }
                await _context.SaveChangesAsync();
                return Json(new { id = channel.Id });
            }
            return BadRequest("An error occured during validation.");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Leave(string? channelId)
        {
            if (channelId == null || _context.Channel == null)
            {
                return BadRequest();
            }
            var channel = await _context.Channel.Include(c => c.Users).Where(c => c.Id == channelId).FirstOrDefaultAsync();
            if (channel == null)
            {
                return NotFound("Channel not found.");
            }
            var currentUser = await _userManager.GetUserAsync(this.User);
            channel.Users.Remove(currentUser);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
