using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prayer.Models;

public class Prayer
{
    [Key]
    public int PrayerId { get; set; }

    public int GroupId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    [StringLength(50)]
    public string CategoryName { get; set; } = null!;

    public DateTime CreationTime { get; set; }

    public DateTime? UpdateTime { get; set; }

    [StringLength(7)]
    public string Recurrence { get; set; } = null!;

    public int? RecurrenceValue { get; set; }

    [StringLength(15)]
    public string Status { get; set; } = null!;

    [ForeignKey("GroupId, CategoryName")]
    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
