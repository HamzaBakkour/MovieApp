using System.ComponentModel.DataAnnotations;
using Movie.Core.Validations;

namespace Movie.Core.Dtos;


public class ActorCreateDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [NotInTheFutureYear(1900)]
    public int BirthYear { get; set; }
}
