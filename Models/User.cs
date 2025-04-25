using Microsoft.AspNetCore.Identity;

namespace prayer.Models;
public class User: IdentityUser
{
    public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();
}
