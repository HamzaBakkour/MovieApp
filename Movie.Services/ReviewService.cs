using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Movie.Contracts;
using Movie.Core;
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

    private void BusinessRulesValidation( Core.DomainEntities.Movie movie) {

        int currentYear = DateTime.UtcNow.Year;
        int movieAge = currentYear - movie.Year;
        int reviewCount = movie.Reviews?.Count ?? 0;

        if (movieAge > 20 && reviewCount > 5)
            throw new MovieExceededMaxReviewsException(movie.Id, 5);
        if (reviewCount > 10)
            throw new MovieExceededMaxReviewsException(movie.Id, 10);

    }

    public async Task<ReviewDto> AddReviewAsync(int movieId, ReviewCreateDto dto, bool trackChanges = true)
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

    public async Task<PagedResult<ReviewDetailsDto>> GetReviewsAsync(int movieId, ReviewPagingParametersDto parameters, bool trackChanges = false)
    {
        var movieExists = await uow.MovieRepository.AnyAsync(movieId, trackChanges);
        if (!movieExists)
            throw new MovieNotFoundException(movieId);

        var pagedReviews = await uow.ReviewRepository.GetPagedReviewsAsync(movieId, parameters.PageNumber, parameters.PageSize, trackChanges);

        var reviewDtos = mapper.Map<List<ReviewDetailsDto>>(pagedReviews);

        return new PagedResult<ReviewDetailsDto>(
            reviewDtos,
            pagedReviews.TotalCount,
            pagedReviews.CurrentPage,
            pagedReviews.PageSize
        );
    }


}
