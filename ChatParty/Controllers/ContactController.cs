using ChatParty.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatParty.Controllers
{
    public class ContactController : Controller
    {
        private readonly ChatPartyAuthContext _context;

        public ContactController(ChatPartyAuthContext context)
        {
            _context = context;
        }


        [AllowAnonymous]
        public async Task<IActionResult> Search(string term)
        {
            var suggestions = await _context
                .Channel
                .Where(
                    c => c.Name.Contains(term)
                )
                .Take(10)
                .Select(
                    c => new { Label = c.Name, Value = c.Id, Type = "Channel" }
                )
                .ToListAsync();
            suggestions.AddRange(
                await _context
                .User
                .Where(
                    m => m.UserName.Contains(term)
                ).Select(
                    m => new { Label = m.UserName, Value = m.Id, Type = "User" }
                ).Take(10).ToListAsync());
            return Json(suggestions);
        }

    }
}
