using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Zip.WebAPI.Models.Responses;

namespace Zip.WebAPI.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorResponse = new UserResponse();

            switch (exception)
            {
                case ArgumentException:
                    errorResponse.AddError(ResponseErrorCodeConstants.ArgumentException.ToString(), exception.Message);
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;

                case DbUpdateException:
                    if (exception.InnerException.Message.Contains("Email"))
                    {
                        errorResponse.AddError(ResponseErrorCodeConstants.ArgumentException.ToString(), "A Uuer with the email address is already exixsts");
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    }
                    else if (exception.InnerException.Message.Contains("unigue"))
                    {
                        errorResponse.AddError(ResponseErrorCodeConstants.ArgumentException.ToString(), "There is an issue with data oprations");
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    }
                    else if (exception.InnerException.Message.Contains("unigue"))
                    {
                        errorResponse.AddError(ResponseErrorCodeConstants.ArgumentException.ToString(), "");
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    }
                    break;

                default:
                    if (exception.Message.Contains("already being tracked"))
                    {
                        errorResponse.AddError(ResponseErrorCodeConstants.ArgumentException.ToString(), "Record is already exists");
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    }
                    else
                    {
                        errorResponse.AddError(ResponseErrorCodeConstants.UnexpectedError.ToString(), " Oops! Something went wrong.");
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    }
                    break;
            }

            var result = JsonSerializer.Serialize(errorResponse);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class GlobalErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalErrorHandlingMiddleware>();
        }
    }
}
