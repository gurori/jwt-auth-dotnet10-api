using Core.Entities;
using Core.Interfaces.Repositories;
using DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;

    public async Task<bool> TryCreateAsync(
        string name,
        string email,
        string passwordHash,
        string role
    )
    {
        bool isUserExist = await _context.Users.AnyAsync(u => u.Email == email);

        if (isUserExist)
            return false;

        var userEntity = new UserEntity()
        {
            Role = role,
            Email = email,
            Name = name,
            PasswordHash = passwordHash,
        };

        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<UserEntity?> GetByEmailAsync(string email)
    {
        return await _context
            .Users.AsNoTracking()
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync();
    }

    public async Task<UserEntity?> GetByIdAsync(Guid id)
    {
        return await _context.Users.AsNoTracking().Where(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<UserEntity>> GetManyByIdAsync(IEnumerable<Guid> ids)
    {
        var userEntities = await _context
            .Users.AsNoTracking()
            .Where(u => ids.Contains(u.Id))
            .ToListAsync();

        return userEntities;
    }

    public async Task<string?> GetRoleByIdAsync(Guid id)
    {
        return await _context
            .Users.AsNoTracking()
            .Where(u => u.Id == id)
            .Select(u => u.Role)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Guid id, string name)
    {
        await _context
            .Users.Where(u => u.Id == id)
            .ExecuteUpdateAsync(s =>
            {
                s.SetProperty(u => u.Name, u => name);
            });
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        await _context.Users.Where(x => x.Id == id).ExecuteDeleteAsync();
    }
}
