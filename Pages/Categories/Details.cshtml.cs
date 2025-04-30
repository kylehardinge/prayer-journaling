using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using prayer.Data;
using prayer.Models;

namespace prayer.Pages.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly prayer.Data.PrayerContext _context;

        public DetailsModel(prayer.Data.PrayerContext context)
        {
            _context = context;
        }

        public Category Category { get; set; } = default!;
        public List<Prayer> Prayers { get; set; } = new List<Prayer>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Category = category;
            }
            Prayers = await _context.Prayer
                .Where(p => p.CategoryId == Category.Id && p.Status != StatusOptions.Archived)
                .ToListAsync();

            return Page();
        }
    }
}
