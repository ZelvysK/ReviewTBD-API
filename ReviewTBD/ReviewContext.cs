using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Models;

public class ReviewContext(DbContextOptions<ReviewContext> options) : DbContext(options)
{
    public DbSet<Anime> Animes { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Studio> Studios { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder) {
    //}
}