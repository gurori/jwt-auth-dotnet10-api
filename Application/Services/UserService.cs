using Application.Interfaces.Services;
using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Auth;
using Core.Interfaces.Repositories;
using Core.Models.Users;

namespace Application.Services;

public class UserService(
    IPasswordHasher passwordHasher,
    IUserRepository userRepository,
    IJwtProvider jwtProvider,
    IMapper mapper
) : IUserService
{
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly IMapper _mapper = mapper;

    public async Task RegisterAsync(string name, string email, string password, string role)
    {
        string hashedPassword = _passwordHasher.Generate(password);

        bool isUserExist = !await _userRepository.TryCreateAsync(name, email, hashedPassword, role);

        if (isUserExist)
            throw new ConflictException("Данный пользователь уже существует");
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var userEntity =
            await _userRepository.GetByEmailAsync(email)
            ?? throw new NotFoundException("Пользователь с данной почтой не зарегистрирован");

        if (!_passwordHasher.Verify(password, userEntity.PasswordHash))
            throw new ConflictException("Неверный пароль");

        var token = await _jwtProvider.GenerateTokenAsync(userEntity.Id, userEntity.Role);

        return token;
    }

    public async Task<UserResponse> GetAsync(Guid id)
    {
        UserEntity user =
            await _userRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"User with '{id}' id not found.");

        return _mapper.Map<UserResponse>(user);
    }

    public async Task UpdateAsync(Guid id, string name)
    {
        await _userRepository.UpdateAsync(id, name);
    }

    public async Task<string> GetRoleByIdAsync(Guid id)
    {
        string role =
            await _userRepository.GetRoleByIdAsync(id)
            ?? throw new NotFoundException("Пользователь не найден");

        return role;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _userRepository.DeleteByIdAsync(id);
    }

    public async Task<IEnumerable<UserResponse>> GetAsync(IEnumerable<Guid> ids)
    {
        IEnumerable<UserEntity> users = await _userRepository.GetManyByIdAsync(ids);

        return _mapper.Map<UserResponse[]>(users);
    }
}
