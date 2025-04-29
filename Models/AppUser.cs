using Microsoft.AspNetCore.Identity;

namespace prayer.Models;
public class AppUser: IdentityUser
{
    

    // Navigations

    public ICollection<Membership> Memberships { get; set; } = new List<Membership>();
}
