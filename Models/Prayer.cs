using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using prayer.Data;


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

public enum ExtraFilterOptions
{
    None,
    Unarchived,
    Today,
}

public struct FilterOptions
{
    public int? GroupId { get; set; }
    public int? CategoryId { get; set; }
    public StatusOptions? Status { get; set; }
    public ExtraFilterOptions? ExtraOptions { get; set; }
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

    public async static Task<List<Prayer>> GetPrayersFiltered(PrayerContext _context, string? userId, FilterOptions options) { 
        List<Prayer> prayers = new List<Prayer>();
        // Get prayers by group
        IQueryable<Membership> mQuery = _context.Membership.Where(m => m.UserId == userId);
        IQueryable<Category> cQuery;
        if (options.GroupId != null) {
            cQuery = mQuery
                .SelectMany(m => m.Group.Categories)
                .Where(c => c.GroupId == options.GroupId);
        } else {
            cQuery = mQuery
                .SelectMany(m => m.Group.Categories);
        }

        // Get prayers by category
        IQueryable<Prayer> pQuery;
        if (options.CategoryId != null) {
            pQuery = cQuery
                .SelectMany(c => c.Prayers)
                .Where(p => p.CategoryId == options.CategoryId);
        } else {
            pQuery = cQuery
                .SelectMany(c => c.Prayers);
        }

        // Get prayers by status
        if (options.Status != null) {
            pQuery = pQuery
                .Where(p => p.Status == options.Status);
        }
        switch (options.ExtraOptions)
        {
            // Get today's prayers
            case ExtraFilterOptions.Today:
                // Prayers can be for today if they are daily,
                // the weekday matches,
                var weekInt = (int)DateTime.Now.DayOfWeek;
                // or the day of the month matches,
                var dayInt = DateTime.Now.Day;
                pQuery = pQuery
                    .Where(c => c.Recurrence == RecurrenceOptions.Daily || (c.Recurrence == RecurrenceOptions.Weekly && c.RecurrenceValue == weekInt) || (c.Recurrence == RecurrenceOptions.Monthly && c.RecurrenceValue == dayInt));
                break;
            case ExtraFilterOptions.Unarchived:
                pQuery = pQuery.Where(p => p.Status != StatusOptions.Archived);
                break;
            case ExtraFilterOptions.None:
            default:
                break;
        }
        return await pQuery.ToListAsync();
    }
}
