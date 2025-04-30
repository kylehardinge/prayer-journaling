using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using prayer.Data;
using prayer.Models;

namespace prayer.Pages.Groups
{
    public class DetailsModel : PageModel
    {
        private readonly prayer.Data.PrayerContext _context;

        public DetailsModel(prayer.Data.PrayerContext context)
        {
            _context = context;
        }

        public Group Group { get; set; } = default!;
        public Dictionary<Category, List<Prayer>> Prayers { get; set; } = new Dictionary<Category, List<Prayer>>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _context.Group.FirstOrDefaultAsync(m => m.Id == id);
            if (group == null)
            {
                return NotFound();
            }
            else
            {
                Group = group;
            }
            var categories = await _context.Category
                .Where(c => c.GroupId == Group.Id)
                .ToListAsync();
            foreach (Category category in categories)
            {
                var catPrayers = await _context.Prayer
                    .Where(p => p.CategoryId == category.Id && p.Status != StatusOptions.Archived)
                    .ToListAsync();
                if (catPrayers == null) {
                    catPrayers = new List<Prayer>();
                }
                Prayers.Add(category, catPrayers);
            }

            return Page();
        }
    }
}
