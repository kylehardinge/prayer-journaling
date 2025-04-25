using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using prayer.Models;

namespace prayer.Data;

public partial class PrayerContext : IdentityDbContext<User>
{
    public DbSet<Category> Category { get; set; } = null!;

    public DbSet<Membership> Membership { get; set; } = null!;

    public DbSet<Group> Group { get; set; } = null!;

    public DbSet<Prayer> Prayer { get; set; } = null!;
    
    public DbSet<Session> Session { get; set; } = null!;


    public PrayerContext()
    {
    }

    public PrayerContext(DbContextOptions<PrayerContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
        builder.Entity<Category>().HasKey(c => new {c.GroupId, c.Name});
        
        // Define the UserGroup relation
        builder.Entity<Membership>().HasKey(m => new {m.UserId, m.GroupId});

        builder.Entity<Membership>()
            .HasOne(m => m.User)
            .WithMany(u => u.Memberships)
            .HasForeignKey(m => m.UserId);

        builder.Entity<Membership>()
            .HasOne(m => m.Group)
            .WithMany(u => u.Memberships)
            .HasForeignKey(m => m.GroupId);
        
    }

}
