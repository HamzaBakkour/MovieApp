using Controller.Tests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Movie.Contracts;
using Movie.Core;
using Movie.Core.Dtos;
using Movie.Core.Exceptions;
using MovieApi.Controllers;


namespace Controller.Tests;

public class MovieControllerTests
{

    private readonly Mock<IServiceManager> serviceManagerMock;
    private readonly Mock<IMovieService> movieServiceMock;
    private readonly MoviesController sut;


    public MovieControllerTests()
    {
        movieServiceMock = new Mock<IMovieService>();
        serviceManagerMock = new Mock<IServiceManager>();
        serviceManagerMock.Setup(s => s.MovieService).Returns(movieServiceMock.Object);


        sut = new MoviesController(serviceManagerMock.Object);

        sut.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

    }


    [Fact]
    public async Task GetMovies_ShouldReturnPagedResult()
    {
        // Arrange
        var parameters = new MoviePagingParametersDto { PageNumber = 1, PageSize = 5 };
        var expectedResult = DtoGenerator.GeneratePagedMovies(parameters.PageSize);

        movieServiceMock
            .Setup(s => s.GetMoviesAsync(parameters, false))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await sut.GetMovies(parameters);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedData = Assert.IsType<PagedResult<MovieDto>>(okResult.Value);
        Assert.Equal(expectedResult.TotalCount, returnedData.TotalCount);
    }


    [Fact]
    public async Task GetMovieDetails_ShouldReturnMovieAllDetailsDto()
    {
        // Arrange
        int movieId = 1;
        var expectedMovie = DtoGenerator.GenerateMovieDetailsDto(movieId);

        movieServiceMock
            .Setup(s => s.GetMovieDetailsAsync(movieId, false))
            .ReturnsAsync(expectedMovie);

        // Act
        var result = await sut.GetMovieDetails(movieId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedMovie = Assert.IsType<MovieAllDetailsDto>(okResult.Value);

        Assert.Equal(expectedMovie.Id, returnedMovie.Id);
        Assert.Equal(expectedMovie.Title, returnedMovie.Title);
        Assert.Equal(expectedMovie.Year, returnedMovie.Year);
        Assert.Equal(expectedMovie.Duration, returnedMovie.Duration);
    }


    [Fact]
    public async Task PostMovie_ShouldReturnCreatedAtActionResult_WhenSuccessful()
    {
        var createDto = DtoGenerator.GenerateCreateMovieDto();

        var returnedDto = new MovieDto(1, createDto.Title, createDto.Year, createDto.Duration);

        movieServiceMock
            .Setup(s => s.AddMovieAsync(createDto, false))
            .ReturnsAsync(returnedDto);

        var result = await sut.PostMovie(createDto);

        var createdAt = Assert.IsType<CreatedAtActionResult>(result.Result);
        var dto = Assert.IsType<MovieDto>(createdAt.Value);
        Assert.Equal(returnedDto.Title, dto.Title);
    }



    [Fact]
    public async Task PostMovie_ShouldThrow_WhenDocumentaryBudgetExceedsLimit()
    {
        // Arrange
        var createDto = DtoGenerator.GenerateInvalidDocumentaryMovie();

        movieServiceMock
            .Setup(s => s.AddMovieAsync(createDto, false))
            .ThrowsAsync(new MovieExceededBudgetException(1_000_000, "Documentary"));

        //Act
        //Assert
        await Assert.ThrowsAsync<MovieExceededBudgetException>(() => sut.PostMovie(createDto));
    }

}
