using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prayer.Data;
using prayer.Models;

namespace prayer.Pages.Prayers
{
    public class EditModel : PageModel
    {
        private readonly prayer.Data.PrayerContext _context;

        public EditModel(prayer.Data.PrayerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Prayer Prayer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prayer =  await _context.Prayer.FirstOrDefaultAsync(m => m.PrayerId == id);
            if (prayer == null)
            {
                return NotFound();
            }
            Prayer = prayer;
           ViewData["GroupId"] = new SelectList(_context.Category, "GroupId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Prayer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrayerExists(Prayer.PrayerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PrayerExists(int id)
        {
            return _context.Prayer.Any(e => e.PrayerId == id);
        }
    }
}
