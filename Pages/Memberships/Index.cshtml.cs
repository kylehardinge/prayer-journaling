using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using prayer.Data;
using prayer.Models;

namespace prayer.Pages.Memberships
{
    public class IndexModel : PageModel
    {
        private readonly prayer.Data.PrayerContext _context;

        public IndexModel(prayer.Data.PrayerContext context)
        {
            _context = context;
        }

        public IList<Membership> Membership { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Membership = await _context.Membership
                .Include(m => m.Group)
                .Include(m => m.User).ToListAsync();
        }
    }
}
