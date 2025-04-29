using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prayer.Models;

public class Session
{
    [Key]
    public int Id { get; set; }

    public string? Notes { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? StopTime { get; set; }

    [ForeignKey("UserId")]
    public AppUser User { get; set; } = null!;

    public ICollection<Prayer> Prayers { get; set; } = null!;
}
