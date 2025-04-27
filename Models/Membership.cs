using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prayer.Models;

public class Membership
{
    [Key]
    public string UserId { get; set; } = null!;

    [Key]
    public int GroupId { get; set; }

    public User? User { get; set; }
    public Group? Group { get; set; }

}
