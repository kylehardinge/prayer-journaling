using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using prayer.Data;
using prayer.Models;

namespace prayer.Pages.Memberships
{
    public class DeleteModel : PageModel
    {
        private readonly PrayerContext _context;

        public DeleteModel(PrayerContext context)
        {
            _context = context;
        }

        // composite key parts, mark SupportsGet so they bind in OnGet
        [BindProperty(SupportsGet = true)]
        public string UserId { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int GroupId { get; set; }

        [BindProperty]
        public Membership Membership { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var membership =  await _context.Membership.FirstOrDefaultAsync(m => m.UserId == UserId && m.GroupId == GroupId);

            if (membership == null)
            {
                return NotFound();
            }
            else
            {
                Membership = membership;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var membership =  await _context.Membership.FindAsync(UserId, GroupId);
            if (membership != null)
            {
                Membership = membership;
                _context.Membership.Remove(Membership);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
