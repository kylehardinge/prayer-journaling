using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prayer.Data;
using prayer.Models;

namespace prayer.Pages.Prayers
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly Data.PrayerContext _context;
        private readonly UserManager<AppUser> _userManager;

        public SelectList RecurrenceList { get; set; } = null!;
        public SelectList StatusList { get; set; } = null!;


        public CreateModel(PrayerContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<SelectListItem> CategoryItems { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);

            var groups = await _context.Membership
                .Where(m => m.UserId == userId)
                .Include(m => m.Group)
                .ThenInclude(g => g.Categories)
                .Select(m => m.Group)
                .ToListAsync();
            
            var items = new List<SelectListItem>();
            foreach (var g in groups)
            {
                Console.WriteLine(g.Name);
                var optGroup = new SelectListGroup { Name = g.Name };
                foreach (var c in g.Categories)
                {
                    Console.WriteLine('\t' + c.Name);
                    items.Add(new SelectListItem() {
                            Value = c.Id.ToString(),
                            Text = c.Name,
                            Group = optGroup
                    });
                }
            }
            CategoryItems = items;
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            RecurrenceList = new SelectList(Enum.GetValues(typeof(RecurrenceOptions)).Cast<RecurrenceOptions>());
            StatusList = new SelectList(Enum.GetValues(typeof(StatusOptions)).Cast<StatusOptions>());

            return Page();
        }

        [BindProperty]
        public Prayer Prayer { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Prayer.CreationTime = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Prayer.Add(Prayer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
