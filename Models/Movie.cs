// Models/Movie.cs
// El modelo define la FORMA de los datos — cada propiedad = columna en la BD.
// Los atributos [DataAnnotations] hacen dos cosas:
//   1. Validan los datos antes de guardarlos
//   2. Controlan cómo se muestran los campos en los formularios HTML

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models;

public class Movie
{
    // EF Core reconoce "Id" automáticamente como Primary Key (clave primaria)
    public int Id { get; set; }

    [StringLength(60, MinimumLength = 2)]
    [Required]
    public string? Title { get; set; }

    // Display: cambia la etiqueta visible en el formulario ("Release Date" en vez de "ReleaseDate")
    // DataType.Date: muestra solo la fecha, sin hora
    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$",
        ErrorMessage = "Genre must start with a capital letter and contain only letters.")]
    [StringLength(30)]
    [Required]
    public string? Genre { get; set; }

    // Range: el precio debe estar entre $1 y $999.99
    // Column: define la precisión en la BD (18 dígitos, 2 decimales)
    [Range(1, 999.99)]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$",
        ErrorMessage = "Rating must start with a capital letter.")]
    [StringLength(5)]
    [Required]
    public string? Rating { get; set; }
}
