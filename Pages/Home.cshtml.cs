using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prayer.Models;

namespace prayer.Pages;

[Authorize]
public class HomeModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly UserManager<AppUser> _userManager;

    public HomeModel(ILogger<IndexModel> logger, UserManager<AppUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public void OnGet()
    {

    }
}
