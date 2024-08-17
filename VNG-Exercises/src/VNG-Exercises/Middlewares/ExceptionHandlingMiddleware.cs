﻿using VNGExercises.Domain.Exceptions;
using Newtonsoft.Json;
using static VNGExercises.Domain.Exceptions.IdentityException;

namespace VNGExercises.Middlewares;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);

        var response = new
        {
            title = GetTitle(exception),
            status = statusCode,
            detail = exception.Message,
            errors = GetErrors(exception),
        };

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            BookException.BookHasBeenDeletedException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            //Application.Exceptions.ValidationException => StatusCodes.Status422UnprocessableEntity,
            FluentValidation.ValidationException => StatusCodes.Status400BadRequest,
            FormatException => StatusCodes.Status422UnprocessableEntity,
            TokenException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(Exception exception) =>
        exception switch
        {
            DomainException applicationException => applicationException.Title,
            _ => "Server Error"
        };

    private static IReadOnlyCollection<VNGExercises.Application.Exceptions.ValidationError> GetErrors(Exception exception)
    {
        IReadOnlyCollection<VNGExercises.Application.Exceptions.ValidationError> errors = null;

        if (exception is VNGExercises.Application.Exceptions.ValidationException validationException)
        {
            errors = validationException.Errors;
        }

        return errors;
    }

}
