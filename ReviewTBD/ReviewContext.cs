using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReviewTBDAPI.Enums;
using ReviewTBDAPI.Models;

namespace ReviewTBDAPI;

public class ReviewContext(DbContextOptions<ReviewContext> options) : DbContext(options)
{
    public DbSet<Anime> Animes { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Studio> Studios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Studio>(entity =>
        {
            entity.Property(e => e.Type).HasConversion(new EnumToStringConverter<StudioType>());
        });
    }
}