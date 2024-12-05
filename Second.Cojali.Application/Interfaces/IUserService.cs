using Second.Cojali.Application.Models;
using Second.Cojali.Domain.Entities;

namespace Second.Cojali.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetByIdAsync(int id);
    Task<UserDto> CreateUserAsync(string name, string email);
    Task UpdateUserAsync(int id, string name, string email);
    Task<bool> UserExistsAsync(int id);
}
