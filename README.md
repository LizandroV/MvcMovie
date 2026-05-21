# MvcMovie — ASP.NET Core MVC (W02 Assignment)

## Assignment Requirements Checklist
- ✅ App title changed to **"Lizandro Movies"** (navbar + browser tab)
- ✅ 3 favorite movies added: *Interstellar*, *The Dark Knight*, *Gladiator*
- ✅ Movie listing title says **"My Movies"** (not "Index")
- ✅ **Year filter** added to search — shows movies from that year or newer
- ✅ **CSS padding** added to all input/select/textarea elements

---

## Project Structure

```
MvcMovie/
├── Controllers/
│   ├── HomeController.cs        ← Home page
│   └── MoviesController.cs      ← CRUD + search with title/genre/year filters
├── Data/
│   └── MvcMovieContext.cs       ← EF Core DbContext (bridge to SQLite)
├── Models/
│   ├── Movie.cs                 ← Data model with validation attributes
│   ├── ErrorViewModel.cs
│   └── SeedData.cs              ← Initial data (7 movies including 3 favorites)
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml
│   │   ├── Privacy.cshtml
│   │   └── Error.cshtml
│   ├── Movies/
│   │   ├── Index.cshtml         ← My Movies list + search form
│   │   ├── Create.cshtml        ← Add new movie form
│   │   ├── Edit.cshtml          ← Edit movie form
│   │   ├── Details.cshtml       ← Read-only movie details
│   │   └── Delete.cshtml        ← Delete confirmation page
│   ├── Shared/
│   │   ├── _Layout.cshtml       ← Master layout (navbar, footer)
│   │   └── _ValidationScriptsPartial.cshtml
│   ├── _ViewImports.cshtml
│   └── _ViewStart.cshtml
├── wwwroot/
│   └── css/site.css             ← Custom styles with form element padding
├── Properties/
│   └── launchSettings.json
├── appsettings.json
├── MvcMovie.csproj
└── Program.cs
```

---

## Setup Steps

### 1. Create the project
```bash
cd "your-course-folder"
dotnet new mvc -o MvcMovie -f net8.0
cd MvcMovie
```

### 2. Copy all provided files into the project folder
Replace the generated files with the ones provided.

### 3. Install packages
```bash
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.SQLite --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.0
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 8.0.0
```

### 4. Trust HTTPS certificate (first time only)
```bash
dotnet dev-certs https --trust
```

### 5. Run the app
```bash
dotnet run
```

Open browser at: **http://localhost:5200/Movies**

---

## MVC Pattern Explained

```
Browser Request
      ↓
  CONTROLLER (MoviesController.cs)
  - Receives HTTP request
  - Calls DbContext to query SQLite
  - Passes data to View
      ↓
  MODEL (Movie.cs)
  - Shape of data
  - Validation rules
      ↓
  VIEW (Index.cshtml)
  - Generates HTML using the model data
  - Sends complete HTML back to browser
      ↓
Browser displays the page
```

---

## Search / Filter Features

| Filter     | How to use                        | Example URL                        |
|------------|-----------------------------------|------------------------------------|
| Title      | Type in the Title field           | `/Movies?searchString=ghost`       |
| Genre      | Select from dropdown              | `/Movies?movieGenre=Comedy`        |
| Year From  | Enter a year number               | `/Movies?yearFrom=2000`            |
| Combined   | Use all three together            | `/Movies?searchString=the&movieGenre=Action&yearFrom=2005` |

---

## CRUD Operations

| Action  | URL                  | HTTP Method | Description              |
|---------|----------------------|-------------|--------------------------|
| List    | /Movies              | GET         | Show all movies          |
| Create  | /Movies/Create       | GET + POST  | Add new movie            |
| Details | /Movies/Details/{id} | GET         | View movie details       |
| Edit    | /Movies/Edit/{id}    | GET + POST  | Modify existing movie    |
| Delete  | /Movies/Delete/{id}  | GET + POST  | Remove movie             |
