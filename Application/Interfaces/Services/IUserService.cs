using Application.Models.Users;

namespace Application.Interfaces.Services;

public interface IUserService
{
    public Task<string> LoginAsync(string email, string password);
    public Task RegisterAsync(string name, string email, string password, string role);
    public Task<UserResponse> GetAsync(Guid id);
    public Task<IEnumerable<UserResponse>> GetAsync(IEnumerable<Guid> ids);
    public Task UpdateAsync(Guid id, string name);
    public Task DeleteAsync(Guid id);
}
