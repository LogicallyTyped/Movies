using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;

namespace Movies.Infrastructure;

public class MoviesDBContext : DbContext
{
    public MoviesDBContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
}
