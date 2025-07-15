using Microsoft.EntityFrameworkCore;
using Movie.Core.DomainEntities;

namespace Movie.Data.DataConfigurations;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }

    public DbSet<Core.DomainEntities.Movie> Movies { get; set; } = default!;
    public DbSet<MovieDetailes> MovieDetailes { get; set; } = default!;
    public DbSet<Actor> Actors { get; set; } = default!;
    public DbSet<Review> Reviews { get; set; } = default!;
    public DbSet<Genre> Genres { get; set; } = default!;
}
