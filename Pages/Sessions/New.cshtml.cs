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
        private readonly UserManager<User> _userManager;

        public User? currentUser;

        [BindProperty]
        public Session Session { get; set; } = default!;

        public NewModel(UserManager<User> userManager, prayer.Data.PrayerContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
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
            _context.SaveChanges();
            Console.WriteLine("Id: " + Session.SessionId);
            return RedirectToPage("./Details", new { id = Session.SessionId });
        }
    }
}
