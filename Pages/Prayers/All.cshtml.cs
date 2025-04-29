using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using prayer.Data;
using prayer.Models;

namespace prayer.Pages.Prayers
{
    [Authorize]
    public class AllModel : PageModel
    {
        private readonly PrayerContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AllModel(PrayerContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Prayer> Prayer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Console.WriteLine("Is this working?");
            var userId = _userManager.GetUserId(User);
            Prayer = await _context.Membership
                .Where(m => m.UserId == userId)
                .SelectMany(m => m.Group.Categories)
                .SelectMany(c => c.Prayers)
                .ToListAsync();
        }
    }
}
