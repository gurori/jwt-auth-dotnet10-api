using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Server.Filters;

public sealed class ApiExceptionFilter : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        if (context.Exception is ApiException apiEx)
        {
            context.Result = new ObjectResult(
                new ProblemDetails
                {
                    Status = apiEx.StatusCode,
                    Title = "Request failed",
                    Detail = apiEx.Message,
                }
            )
            {
                StatusCode = apiEx.StatusCode,
            };

            context.ExceptionHandled = true;
        }

        return Task.CompletedTask;
    }
}
