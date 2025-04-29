using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using prayer.Data;
using prayer.Models;

namespace prayer.Pages.Sessions
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly PrayerContext _context;
        private readonly UserManager<AppUser> _userManager;

        public IndexModel(PrayerContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Session> Session { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Session = await _context.Session.Where(c => c.User.Id == _userManager.GetUserId(User)).ToListAsync();
        }
    }
}
