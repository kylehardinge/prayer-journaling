using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace prayer.Models;

public class Category
{
    [Key]
    public int GroupId { get; set; }

    [Key]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(7)]
    public string Recurrence { get; set; } = null!;

    public int? RecurrenceValue { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual ICollection<Prayer> Prayers { get; set; } = new List<Prayer>();
}
