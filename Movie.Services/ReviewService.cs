using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Movie.Contracts;
using Movie.Core.DomainContracts;
using Movie.Core.DomainEntities;
using Movie.Core.Dtos;
using Movie.Core.Exceptions;

namespace Movie.Services;

public class ReviewService : IReviewService
{

    private IUnitOfWork uow;
    private readonly IMapper mapper;

    public ReviewService(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }

    private bool BusinessRulesValidation( Core.DomainEntities.Movie movie) {

        int currentYear = DateTime.UtcNow.Year;
        int movieAge = currentYear - movie.Year;
        int reviewCount = movie.Reviews?.Count ?? 0;

        if (movieAge > 20 && reviewCount > 5)
            throw new MovieExceededMaxReviewsException(movie.Id, 5);
        if (reviewCount > 10)
            throw new MovieExceededMaxReviewsException(movie.Id, 10);

        return true;

    }

    public async Task<ReviewDto> AddReviewAsync(int movieId, ReviewCreateDto dto, bool trackChanges = false)
    {

        var movie = await uow.MovieRepository.GetReviewsAsync(movieId, trackChanges);

        if (movie == null) throw new MovieNotFoundException(movieId);

        BusinessRulesValidation(movie);
            

        var reviewEntity = mapper.Map<Review>(dto);
        reviewEntity.MovieId = movieId;

        await uow.ReviewRepository.AddAsync(reviewEntity);

        await uow.CompleteAsync();

        return mapper.Map<ReviewDto>(reviewEntity);

    }

    public async Task<IEnumerable<ReviewDetailsDto>> GetReviewsAsync(int movieId, bool trackChanges = false)
    {
        var movie = await uow.MovieRepository.GetReviewsAsync(movieId, trackChanges);

        if (movie == null) throw new MovieNotFoundException(movieId);

        return mapper.Map<IEnumerable<ReviewDetailsDto>>(movie.Reviews);
    }
}
