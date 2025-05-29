using Microsoft.EntityFrameworkCore;

namespace Movies.Infrastructure;

internal class MoviesDBContext : DbContext
{
    public MoviesDBContext(DbContextOptions<MoviesDBContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoviesDBContext).Assembly);
    }
    public DbSet<Movie> Movies { get; set; } = null!;
}
{

}
