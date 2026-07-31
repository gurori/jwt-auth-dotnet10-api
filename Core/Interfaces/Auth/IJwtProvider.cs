namespace Core.Interfaces.Auth;

public interface IJwtProvider
{
    public Task<string> GenerateTokenAsync(Guid id, string role);
}
