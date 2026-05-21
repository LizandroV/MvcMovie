using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

var builder = WebApplication.CreateBuilder(args);

// ── Registrar MVC (Controllers + Views) ──────────────────────────────────────
builder.Services.AddControllersWithViews();

// ── Registrar DbContext con SQLite ────────────────────────────────────────────
builder.Services.AddDbContext<MvcMovieContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("MvcMovieContext")
        ?? throw new InvalidOperationException("Connection string 'MvcMovieContext' not found.")));

var app = builder.Build();

// ── Seed de datos iniciales ───────────────────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// ── Pipeline HTTP ─────────────────────────────────────────────────────────────
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Ruta por defecto: /Controller/Action/id
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
