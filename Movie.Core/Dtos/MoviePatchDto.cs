using System.ComponentModel.DataAnnotations;
using Movie.Core.Validations;

namespace Movie.Core.Dtos;

public class MoviePatchDto
{
    [StringLength(150)]
    public string? Title { get; set; }

    [NotInTheFutureYear(1800)]
    public int? Year { get; set; }
    public MovieDetailesPatchDto? Detailes { get; set; }
}
