using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace prayer.Models;

public class Membership
{
    public string UserId { get; set; } = null!;

    public int GroupId { get; set; }

    public DateTime Enrolled { get; set; }


    // Navigations

    [ValidateNever]
    public AppUser User { get; set; } = null!;
    [ValidateNever]
    public Group Group { get; set; } = null!;

}
