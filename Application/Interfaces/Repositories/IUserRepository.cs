using Core.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> TryCreateAsync(
            string name,
            string email,
            string passwordHash,
            string role
        );

        public Task<UserEntity?> GetByEmailAsync(string email);

        public Task<UserEntity?> GetByIdAsync(Guid id);
        public Task UpdateAsync(Guid id, string name);
        public Task<string?> GetRoleByIdAsync(Guid id);
        public Task<IEnumerable<UserEntity>> GetManyByIdAsync(IEnumerable<Guid> ids);
        public Task DeleteByIdAsync(Guid id);
    }
}
