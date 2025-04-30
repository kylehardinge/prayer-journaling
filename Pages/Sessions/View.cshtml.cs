using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using prayer.Data;
using prayer.Models;

namespace prayer.Pages.Sessions
{
    public class ViewModel : PageModel
    {
        private readonly prayer.Data.PrayerContext _context;

        public ViewModel(prayer.Data.PrayerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Session Session { get; set; } = default!;
        public List<Prayer> SessionPrayers { get; set; } = new List<Prayer>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Session.FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }
            else
            {
                Session = session;
            }
            if (Session.StopTime != null) {
                return Redirect("./Details");
            }
            SessionPrayers = await _context.Session
                .SelectMany(s => s.Prayings)
                .Where(s => s.SessionId == Session.Id)
                .Select(p => p.Prayer)
                .ToListAsync();
            
            return Page();
            // if (Session.StopTime == null) {
            //     return Partial("_ViewInProgress");
            // }
            // return Partial("_ViewComplete");
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Session.StopTime != null)
            {
                return NotFound();
            }

            // Set the stop time
            Session.StopTime = DateTime.Now;
            _context.Attach(Session).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(Session.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Home");
        }

        private bool SessionExists(int id)
        {
            return _context.Session.Any(e => e.Id == id);
        }
    }
}
