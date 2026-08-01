namespace Core.Exceptions
{
    public class ApiException(string message, int statusCode) : ApplicationException(message)
    {
        public int StatusCode { get; } = statusCode;
    }

    public sealed class NotFoundException(string message) : ApiException(message, 404) { }

    public sealed class ConflictException(string message) : ApiException(message, 409) { }

    public sealed class UnauthorizedException(string message = "Unauthorized")
        : ApiException(message, 401) { }

    public sealed class ForbiddenException(string message = "Forbidden")
        : ApiException(message, 403) { }
}
