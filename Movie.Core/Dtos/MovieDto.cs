using System.ComponentModel.DataAnnotations;

namespace Movie.Core.Dtos;


public record MovieDto (int Id, string Title, int Year, int Duration);

