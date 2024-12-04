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

    public IEnumerable<UserDto> GetAllUsers()
    {
        var users = _userRepository.GetAllUsers();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public UserDto GetById(int id)
    {
        var user = FindUserOrThrow(id);
        return _mapper.Map<UserDto>(user);
    }

    public UserDto CreateUser(string name, string email)
    {
        ValidateUserInput(name, email);

        var newUser = new User { Name = name, Email = email };
        _userRepository.AddUser(newUser);

        _emailService.SendEmail(email, "Welcome!", $"Hello {name}, your account has been created.");

        return _mapper.Map<UserDto>(newUser);
    }

    public void UpdateUser(int id, string name, string email)
    {
        ValidateUserInput(name, email);

        var user = FindUserOrThrow(id);

        user.Name = name;
        user.Email = email;
        _userRepository.UpdateUser(user);
    }

    public bool UserExists(int id) => _userRepository.Exists(id);

    private User FindUserOrThrow(int id)
    {
        return _userRepository.GetById(id) ?? throw new KeyNotFoundException($"User with ID {id} not found.");
    }

    private static void ValidateUserInput(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));
    }
}
