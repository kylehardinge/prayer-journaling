using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prayer.Models;

public class Session
{
    [Key]
    public int SessionId { get; set; }

    public string? Notes { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? StopTime { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    public virtual ICollection<Prayer> Prayers { get; set; } = new List<Prayer>();
}
