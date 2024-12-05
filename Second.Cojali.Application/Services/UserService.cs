using AutoMapper;
using Second.Cojali.Application.Interfaces;
using Second.Cojali.Application.Models;
using Second.Cojali.Domain.Entities;
using Second.Cojali.Domain.Ports;

namespace Second.Cojali.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public UserService(IUserRepository userRepository, IMapper mapper, IEmailService emailService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _emailService = emailService;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await FindUserOrThrowAsync(id);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateUserAsync(string name, string email)
    {
        ValidateUserInput(name, email);

        var newUser = new User { Name = name, Email = email };
        await _userRepository.AddUserAsync(newUser);

        await _emailService.SendEmailAsync(email, "Welcome!", $"Hello {name}, your account has been created.");

        return _mapper.Map<UserDto>(newUser);
    }

    public async Task UpdateUserAsync(int id, string name, string email)
    {
        ValidateUserInput(name, email);

        var user = await FindUserOrThrowAsync(id);

        user.Name = name;
        user.Email = email;
        await _userRepository.UpdateUserAsync(user);
    }

    public async Task<bool> UserExistsAsync(int id) => await _userRepository.ExistsAsync(id);

    private async Task<User> FindUserOrThrowAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id)
               ?? throw new KeyNotFoundException($"User with ID {id} not found.");
    }

    private static void ValidateUserInput(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));
    }
}
