using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReviewTBDAPI.Enums;
using ReviewTBDAPI.Models;

namespace ReviewTBDAPI;

public class ReviewContext(DbContextOptions<ReviewContext> options)
    : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Studio> Studios { get; set; }
    public DbSet<Media> Media { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Studio>(entity =>
        {
            entity.Property(e => e.Type).HasConversion(new EnumToStringConverter<StudioType>());
        });
        modelBuilder.Entity<Media>(entity =>
        {
            entity.Property(e => e.MediaType).HasConversion(new EnumToStringConverter<MediaType>());
        });

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(e => e.Role)
                .HasDefaultValue(Role.User)
                .HasConversion(new EnumToStringConverter<Role>());

            entity.Property(e => e.FirstTimeLogin).HasDefaultValue(true);
        });

        base.OnModelCreating(modelBuilder);
    }
}