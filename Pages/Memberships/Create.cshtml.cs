using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using prayer.Data;
using prayer.Models;

namespace prayer.Pages.Memberships
{
    public class CreateModel : PageModel
    {
        private readonly prayer.Data.PrayerContext _context;

        [BindProperty(SupportsGet = true)]
        public int? FromGroupId { get; set; }

        public CreateModel(prayer.Data.PrayerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (FromGroupId != null) {
                Membership = new Membership
                {
                    GroupId = (int)FromGroupId,
                };
            }
        ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return Page();
        }

        [BindProperty]
        public Membership Membership { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Membership.Enrolled = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Membership.Add(Membership);
            await _context.SaveChangesAsync();

            if (FromGroupId != null) {
                return RedirectToPage("../Groups/Details", new {id = FromGroupId});
            }

            return RedirectToPage("./Index");
        }
    }
}
