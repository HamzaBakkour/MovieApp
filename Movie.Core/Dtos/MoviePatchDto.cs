namespace Movie.Core.Dtos;

public class MoviePatchDto
{
    public string? Title { get; set; }
    public int? Year { get; set; }
    public MovieDetailesPatchDto? Detailes { get; set; }
}
