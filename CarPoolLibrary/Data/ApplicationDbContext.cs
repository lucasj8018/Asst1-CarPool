using CarPoolLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarPoolLibrary.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Member>? Members { get; set; }
    public DbSet<Vehicle>? Vehicles { get; set; }
    public DbSet<Manifest>? Manifests { get; set; }
    public DbSet<Trip>? Trips { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        // builder.Entity<Trip>().HasKey(t => new { t.TripId, t.VehicleId });
        // builder.Entity<Manifest>().HasKey(m => new { m.ManifestId, m.MemberId });
        builder.Entity<Trip>().HasIndex(e => new { e.TripId, e.VehicleId }).IsUnique();
        builder.Entity<Manifest>().HasIndex(e => new { e.ManifestId, e.MemberId }).IsUnique();

        base.OnModelCreating(builder);
        builder.Entity<Member>().ToTable("Member");
        builder.Entity<Vehicle>().ToTable("Vehicle");
        builder.Entity<Trip>().ToTable("Trip");
        builder.Entity<Manifest>().ToTable("Manifest");

        builder.Seed();
    }
}
