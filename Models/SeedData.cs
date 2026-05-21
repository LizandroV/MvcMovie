// Models/SeedData.cs
// SeedData carga películas iniciales en la BD al arrancar la app.
// Si la tabla ya tiene datos, no hace nada (evita duplicados).

using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new MvcMovieContext(
            serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>());

        // Asegurar que la BD y las tablas existan (aplica migraciones pendientes)
        context.Database.EnsureCreated();

        // Si ya hay películas, salir sin hacer nada
        if (context.Movie.Any())
            return;

        context.Movie.AddRange(
            // ── Películas del tutorial ────────────────────────────────────────
            new Movie
            {
                Title        = "When Harry Met Sally",
                ReleaseDate  = DateTime.Parse("1989-02-12"),
                Genre        = "Romantic Comedy",
                Rating       = "R",
                Price        = 7.99M
            },
            new Movie
            {
                Title        = "Ghostbusters",
                ReleaseDate  = DateTime.Parse("1984-03-13"),
                Genre        = "Comedy",
                Rating       = "PG",
                Price        = 8.99M
            },
            new Movie
            {
                Title        = "Ghostbusters II",
                ReleaseDate  = DateTime.Parse("1989-06-16"),
                Genre        = "Comedy",
                Rating       = "PG",
                Price        = 9.99M
            },
            new Movie
            {
                Title        = "Rio Bravo",
                ReleaseDate  = DateTime.Parse("1959-04-15"),
                Genre        = "Western",
                Rating       = "NR",
                Price        = 3.99M
            },

            // ── ✅ Mis 3 películas favoritas (requerimiento del assignment) ───
            new Movie
            {
                Title        = "Interstellar",
                ReleaseDate  = DateTime.Parse("2014-11-07"),
                Genre        = "Sci-Fi",
                Rating       = "PG13",
                Price        = 14.99M
            },
            new Movie
            {
                Title        = "The Dark Knight",
                ReleaseDate  = DateTime.Parse("2008-07-18"),
                Genre        = "Action",
                Rating       = "PG13",
                Price        = 12.99M
            },
            new Movie
            {
                Title        = "Gladiator",
                ReleaseDate  = DateTime.Parse("2000-05-05"),
                Genre        = "Drama",
                Rating       = "R",
                Price        = 9.99M
            }
        );

        context.SaveChanges();
    }
}
