using System;
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
    public class DetailsModel : PageModel
    {
        private readonly prayer.Data.PrayerContext _context;

        public DetailsModel(prayer.Data.PrayerContext context)
        {
            _context = context;
        }

        public Prayer Prayer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prayer = await _context.Prayer.FirstOrDefaultAsync(m => m.PrayerId == id);
            if (prayer == null)
            {
                return NotFound();
            }
            else
            {
                Prayer = prayer;
            }
            return Page();
        }
    }
}
