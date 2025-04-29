using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace prayer.Models;

public enum RecurrenceOptions
{
    None = 0,
    Daily = 1,
    Weekly = 2,
    Monthly = 3
}

public enum StatusOptions
{
    Unanswered = 0,
    Answered = 1,
    Archived = -1
}

public class Prayer
{
    [Key]
    public int Id { get; set; }

    public int CategoryId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime? UpdateTime { get; set; }

    public RecurrenceOptions Recurrence { get; set; }

    public int? RecurrenceValue { get; set; }

    public StatusOptions Status { get; set; }


    // Navigations

    [ValidateNever]
    public Category Category { get; set; } = null!;

    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}
