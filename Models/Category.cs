using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace prayer.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    public int GroupId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;
    

    // Navigations

    [ValidateNever]
    public Group Group { get; set; } = null!;

    public ICollection<Prayer> Prayers { get; set; } = new List<Prayer>();
}
