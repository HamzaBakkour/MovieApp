﻿namespace Movie.Core.DomainEntities;

public class Review
{
    public int Id { get; set; }
    public string ReviewerName { get; set; } = null!;
    public string? Comment { get; set; }
    public int Rating { get; set; }

    public int MovieId { get; set; }

    public Movie Movie { get; set; } = null!;
}
