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
            var suggestions = await _context.Channel.Where(
                mg => mg.Name.Contains(term)
                ).Take(10).Select(mg => mg.Name).ToListAsync();
            suggestions.AddRange(
                await _context.User.Where(
                    mg => mg.UserName.Contains(term)
                ).Select(
                    m => m.UserName
                ).Take(10).ToListAsync());
            return Json(suggestions);
        }

    }
}
