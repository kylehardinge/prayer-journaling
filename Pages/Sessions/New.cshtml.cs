using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using prayer.Data;
using prayer.Models;

namespace prayer.Pages.Sessions
{
    [Authorize]
    public class NewModel : PageModel
    {
        private readonly prayer.Data.PrayerContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AppUser? currentUser;

        [BindProperty]
        public Session Session { get; set; } = default!;

        public NewModel(UserManager<AppUser> userManager, prayer.Data.PrayerContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Session = new Session();
            var task = _userManager.GetUserAsync(User);
            task.Wait();
            if (task.Result == null) {
                return RedirectToPage("Index");
            }
            Session.User = task.Result;
            
            Session.StartTime = DateTime.Now;
            _context.Session.Add(Session);
            var prayers = await Prayer.GetPrayersFiltered(_context, Session.User.Id, new FilterOptions() {Status = StatusOptions.Unanswered, ExtraOptions = ExtraFilterOptions.Today});
            foreach (Prayer prayer in prayers)
            {
                var praying = new Praying() {
                    Session = Session,
                    Prayer = prayer,
                    Added = DateTime.Now,
                };
                _context.Praying.Add(praying);
            }
            _context.SaveChanges();
            return RedirectToPage("./Details", new { id = Session.Id });
        }
    }
}
