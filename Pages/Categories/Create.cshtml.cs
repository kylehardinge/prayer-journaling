using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using prayer.Data;
using prayer.Models;

namespace prayer.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly prayer.Data.PrayerContext _context;

        public CreateModel(prayer.Data.PrayerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["GroupId"] = new SelectList(_context.Group, "GroupId", "GroupId");
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Category.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
