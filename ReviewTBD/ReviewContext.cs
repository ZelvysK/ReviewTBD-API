using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Models;

public class ReviewContext(DbContextOptions<ReviewContext> options) : DbContext(options)
{
    public DbSet<Anime> Animes { get; set; }
    public DbSet<AnimeStudio> AnimeStudios { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<GameStudio> GameStudios { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieStudio> MovieStudios { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder) {
    //}
}