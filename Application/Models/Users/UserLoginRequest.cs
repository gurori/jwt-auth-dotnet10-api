namespace Application.Models.Users;

public sealed record UserLoginRequest(string Email, string Password);
