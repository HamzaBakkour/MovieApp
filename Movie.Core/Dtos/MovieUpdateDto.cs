﻿using System.ComponentModel.DataAnnotations;
//using MovieApi.Validations;

namespace Movie.Core.Dtos;


public class MovieUpdateDto
{
    [Required]
    [StringLength(150)]
    public string Title { get; set; } = null!;

    //[NotInTheFutureYear(1800)]
    public int Year { get; set; }

    [Range(1, 600)]
    public int Duration { get; set; }
}
