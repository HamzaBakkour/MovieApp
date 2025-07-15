namespace Movie.Core.Dtos;




public record ReviewDto( string ReviewerName,
                         string? Comment,
                         int Rating);