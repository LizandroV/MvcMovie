// Data/MvcMovieContext.cs
// DbContext es el puente entre C# y la base de datos.
// DbSet<Movie> representa la tabla "Movie" en SQLite.

using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data;

public class MvcMovieContext : DbContext
{
    public MvcMovieContext(DbContextOptions<MvcMovieContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movie { get; set; } = default!;
}
