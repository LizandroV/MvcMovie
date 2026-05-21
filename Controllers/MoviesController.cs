// Controllers/MoviesController.cs
// El Controller recibe requests HTTP, llama a la BD vía DbContext,
// y pasa los datos a las Views para generar el HTML.
//
// Patrón de rutas:  /Movies          → Index
//                   /Movies/Details/5 → Details(5)
//                   /Movies/Create    → Create (GET y POST)
//                   /Movies/Edit/5    → Edit(5) (GET y POST)
//                   /Movies/Delete/5  → Delete(5) (GET y POST)

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers;

public class MoviesController : Controller
{
    private readonly MvcMovieContext _context;

    // Inyección de dependencias: ASP.NET Core provee el DbContext automáticamente
    public MoviesController(MvcMovieContext context)
    {
        _context = context;
    }

    // ─── GET /Movies ──────────────────────────────────────────────────────────
    // Muestra la lista de películas con filtros opcionales de búsqueda.
    // Los parámetros vienen del formulario de búsqueda en la URL:
    //   /Movies?searchString=ghost&movieGenre=Comedy&yearFrom=2000
    public async Task<IActionResult> Index(
        string movieGenre,
        string searchString,
        int?   yearFrom)
    {
        // Consulta base: todas las películas
        var movies = from m in _context.Movie select m;

        // ── Filtro 1: por título ──────────────────────────────────────────────
        if (!string.IsNullOrEmpty(searchString))
        {
            // Contains es case-insensitive en SQLite
            movies = movies.Where(s => s.Title!.Contains(searchString));
        }

        // ── Filtro 2: por género ──────────────────────────────────────────────
        if (!string.IsNullOrEmpty(movieGenre))
        {
            movies = movies.Where(x => x.Genre == movieGenre);
        }

        // ── Filtro 3: por año (año >= yearFrom) ── ✅ Requerimiento adicional ─
        if (yearFrom.HasValue)
        {
            movies = movies.Where(m => m.ReleaseDate.Year >= yearFrom.Value);
        }

        // Construir el dropdown de géneros con valores únicos de la BD
        var genreQuery = await _context.Movie
            .OrderBy(m => m.Genre)
            .Select(m => m.Genre)
            .Distinct()
            .ToListAsync();

        // ViewData pasa datos adicionales a la View sin necesidad de un ViewModel
        ViewData["MovieGenre"]  = new SelectList(genreQuery);
        ViewData["SearchString"] = searchString;
        ViewData["YearFrom"]    = yearFrom;

        return View(await movies.ToListAsync());
    }

    // ─── GET /Movies/Details/5 ────────────────────────────────────────────────
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
            return NotFound();

        return View(movie);
    }

    // ─── GET /Movies/Create ───────────────────────────────────────────────────
    // Muestra el formulario vacío para crear una nueva película
    public IActionResult Create()
    {
        return View();
    }

    // ─── POST /Movies/Create ──────────────────────────────────────────────────
    // Recibe los datos del formulario, valida y guarda en la BD
    // [Bind] especifica qué propiedades se aceptan del formulario (seguridad)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
    {
        if (ModelState.IsValid)
        {
            _context.Add(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    // ─── GET /Movies/Edit/5 ───────────────────────────────────────────────────
    // Muestra el formulario con los datos actuales de la película
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var movie = await _context.Movie.FindAsync(id);

        if (movie == null)
            return NotFound();

        return View(movie);
    }

    // ─── POST /Movies/Edit/5 ─────────────────────────────────────────────────
    // Recibe los datos modificados y actualiza la BD
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
    {
        if (id != movie.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    // ─── GET /Movies/Delete/5 ─────────────────────────────────────────────────
    // Muestra la página de confirmación antes de eliminar
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
            return NotFound();

        return View(movie);
    }

    // ─── POST /Movies/Delete/5 ────────────────────────────────────────────────
    // Elimina la película de la BD después de que el usuario confirma
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var movie = await _context.Movie.FindAsync(id);

        if (movie != null)
        {
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    // Método privado de ayuda para verificar si una película existe
    private bool MovieExists(int id)
    {
        return _context.Movie.Any(e => e.Id == id);
    }
}
