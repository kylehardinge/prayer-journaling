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
    public class EditModel : PageModel
    {
        private readonly PrayerContext _context;
        private readonly UserManager<AppUser> _userManager;

        public EditModel(PrayerContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Prayer Prayer { get; set; } = default!;

        public SelectList RecurrenceList { get; set; } = null!;
        public SelectList StatusList { get; set; } = null!;
        public List<SelectListItem> CategoryItems { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prayer =  await _context.Prayer.FirstOrDefaultAsync(m => m.Id == id);
            if (prayer == null)
            {
                return NotFound();
            }
            Prayer = prayer;
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Prayer.UpdateTime = DateTime.Now;

            _context.Attach(Prayer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrayerExists(Prayer.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./All");
        }

        private bool PrayerExists(int id)
        {
            return _context.Prayer.Any(e => e.Id == id);
        }
    }
}
