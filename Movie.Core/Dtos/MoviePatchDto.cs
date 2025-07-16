using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Dtos;

public class MoviePatchDto
{
    public string? Title { get; set; }
    public int? Year { get; set; }
}
