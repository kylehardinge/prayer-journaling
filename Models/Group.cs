using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using prayer.Data;

namespace prayer.Models;

public class Group
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string? Description { get; set; }
    

    // Navigations

    public ICollection<Category> Categories { get; set; } = new List<Category>();

    public ICollection<Membership> Memberships { get; set; } = new List<Membership>();

    /// <summary>
    /// Adds some defaults to the database
    /// Namely, it creates default categories and adds the current user to the group
    /// NOTE: Does not save the changes made to the database
    /// <summary>
    public void AddDefaults(PrayerContext _context, AppUser user) {
        this.AddDefaultCategoriesAsync(_context);
        this.AddMembershipAsync(_context, user);
    }

    /// <summary>
    /// Creates some default categories for a group
    /// NOTE: Does not save the changes made to the database
    /// <summary>
    public async void AddDefaultCategoriesAsync(PrayerContext _context) {
        List<string> names = ["Personal", "Church", "Outreach", "Special Needs"];
        foreach (string name in names)
        {
            var category = new Category() {
                Name = name,
                Group = this,
            };
            await _context.Category.AddAsync(category);
        }
    }

    /// <summary>
    /// Adds a user to the database
    /// NOTE: Does not save the changes made to the database
    /// <summary>
    public async void AddMembershipAsync(PrayerContext _context, AppUser user) {
        var membership = new Membership() {
            User = user,
            Group = this,
            Enrolled = DateTime.Now,
        };
        await _context.Membership.AddAsync(membership);
    }
}
