using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
}
