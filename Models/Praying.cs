using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace prayer.Models;

public class Praying
{
    public int SessionId { get; set; }

    public int PrayerId { get; set; }

    public DateTime Added { get; set; }


    // Navigations

    [ValidateNever]
    public Session Session { get; set; } = null!;
    [ValidateNever]
    public Prayer Prayer { get; set; } = null!;

}
