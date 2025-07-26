using System.ComponentModel.DataAnnotations;

namespace Movie.Core.Dtos;

public class MovieDetailesPatchDto
{
    [StringLength(250)]
    public string? Synopsis { get; set; }
    
    [StringLength(80)]
    public string? Language { get; set; }
    
    [Range(0, int.MaxValue)]
    public int? Budget { get; set; }
}