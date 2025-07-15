namespace Movie.Core.DomainEntities;

public class MovieDetailes
{
    public int Id { get; set; }
    public string Synopsis { get; set; } = null!;
    public string Language { get; set; } = null!;
    public int Budget { get; set; }

    public int MovieId { get; set; }

    public Movie Movie { get; set; } = null!;
}
