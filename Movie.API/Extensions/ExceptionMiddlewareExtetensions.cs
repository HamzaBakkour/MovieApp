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
                        case MovieExceededMaxReviewsException movieExceededMaxReviewsException:
                            statusCode = StatusCodes.Status400BadRequest;
                            problemDetails = problemDetailsFactory.CreateProblemDetails(
                                    context,
                                    statusCode,
                                    title: "Max Reviews Reached",
                                    detail: movieExceededMaxReviewsException.Message,
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