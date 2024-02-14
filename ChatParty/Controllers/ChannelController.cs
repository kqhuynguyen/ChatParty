using ChatParty.Areas.Identity.Data;
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

        [AllowAnonymous]
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
    }
}
