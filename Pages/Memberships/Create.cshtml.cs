using System;
using System.ComponentModel;
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

        public CreateModel(prayer.Data.PrayerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["GroupId"] = new SelectList(_context.Group, "GroupId", "GroupId");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Membership Membership { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Membership.User = await _context.Users.FindAsync(Membership.UserId);
            Membership.Group = await _context.Group.FindAsync(Membership.GroupId);
            foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(Membership))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(Membership);
                Console.WriteLine("{0}={1}", name, value);
            }
            if (!ModelState.IsValid)
            {
                
                Console.WriteLine("Failed");
                return Page();
            }
            Console.WriteLine("Passed Validity");

            _context.Membership.Add(Membership);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
