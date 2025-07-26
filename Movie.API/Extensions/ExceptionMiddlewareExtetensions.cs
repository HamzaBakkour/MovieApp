using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Movie.Core.Exceptions;

namespace Movie.API.Extensions;

public static class ExceptionMiddlewareExtetensions
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    var problemDetailsFactory = app.Services.GetRequiredService<ProblemDetailsFactory>();

                    ProblemDetails problemDetails;
                    int statusCode;

                    switch (contextFeature.Error)
                    {

                        case MovieNotFoundException companyNotFoundException:
                            statusCode = StatusCodes.Status404NotFound;
                            problemDetails = problemDetailsFactory.CreateProblemDetails(
                                    context,
                                    statusCode,
                                    title: companyNotFoundException.Title,
                                    detail: companyNotFoundException.Message,
                                    instance: context.Request.Path);
                            break;

                        case ActorNotFoundException actorNotFoundException:
                            statusCode = StatusCodes.Status404NotFound;
                            problemDetails = problemDetailsFactory.CreateProblemDetails(
                                    context,
                                    statusCode,
                                    title: "Actor Not Found",
                                    detail: actorNotFoundException.Message,
                                    instance: context.Request.Path);
                            break;


                        case MovieExceededMaxReviewsException movieExceededMaxReviewsException:
                            statusCode = StatusCodes.Status400BadRequest;
                            problemDetails = problemDetailsFactory.CreateProblemDetails(
                                    context,
                                    statusCode,
                                    title: "Max Reviews Reached",
                                    detail: movieExceededMaxReviewsException.Message,
                                    instance: context.Request.Path);
                            break;

                        case ActorAlreayAssignedException actorAlreayAssignedException:
                            statusCode = StatusCodes.Status400BadRequest;
                            problemDetails = problemDetailsFactory.CreateProblemDetails(
                                    context,
                                    statusCode,
                                    title: "Actor Already Assigned",
                                    detail: actorAlreayAssignedException.Message,
                                    instance: context.Request.Path);
                            break;

                        case MovieExceededMaxActorsException movieExceededMaxActorsException:
                            statusCode = StatusCodes.Status400BadRequest;
                            problemDetails = problemDetailsFactory.CreateProblemDetails(
                                    context,
                                    statusCode,
                                    title: "Max Actors Reached",
                                    detail: movieExceededMaxActorsException.Message,
                                    instance: context.Request.Path);
                            break;

                        case MovieAlreadyExistException movieAlreadyExistException:
                            statusCode = StatusCodes.Status400BadRequest;
                            problemDetails = problemDetailsFactory.CreateProblemDetails(
                                    context,
                                    statusCode,
                                    title: "Movie Already Exist",
                                    detail: movieAlreadyExistException.Message,
                                    instance: context.Request.Path);
                            break;

                        case MovieExceededBudgetException movieExceededBudgetException:
                            statusCode = StatusCodes.Status400BadRequest;
                            problemDetails = problemDetailsFactory.CreateProblemDetails(
                                    context,
                                    statusCode,
                                    title: "Movie Exceeded Budget",
                                    detail: movieExceededBudgetException.Message,
                                    instance: context.Request.Path);
                            break;

                        case InvaildGenreException invaildGenreException:
                            statusCode = StatusCodes.Status400BadRequest;
                            problemDetails = problemDetailsFactory.CreateProblemDetails(
                                    context,
                                    statusCode,
                                    title: "Invalid Genre",
                                    detail: invaildGenreException.Message,
                                    instance: context.Request.Path);
                            break;

                        case GenreRequiredException genreRequiredException:
                            statusCode = StatusCodes.Status400BadRequest;
                            problemDetails = problemDetailsFactory.CreateProblemDetails(
                                    context,
                                    statusCode,
                                    title: "Genre Required",
                                    detail: genreRequiredException.Message,
                                    instance: context.Request.Path);
                            break;

                        default:
                            statusCode = StatusCodes.Status500InternalServerError;
                            problemDetails = problemDetailsFactory.CreateProblemDetails(
                                    context,
                                    statusCode,
                                    title: "Internal Server Error",
                                    detail: contextFeature.Error.Message,
                                    instance: context.Request.Path);
                            break;

                    }

                    context.Response.StatusCode = statusCode;
                    await context.Response.WriteAsJsonAsync(problemDetails);
                }
            });
        });
    }
}