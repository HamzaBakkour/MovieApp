using System.ComponentModel.DataAnnotations;

namespace Movie.Core.Dtos;


public class MovieDetailesCreateDto
{

    [StringLength(250)]
    public string Synopsis { get; set; } = null!;

    [StringLength(80)]
    public string Language { get; set; } = null!;

    [Range(0, int.MaxValue)]
    public int Budget { get; set; }

}
