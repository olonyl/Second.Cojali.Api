using Second.Cojali.Domain.Entities;
using Newtonsoft.Json;
using Second.Cojali.Application.Interfaces;
using Second.Cojali.Infrastructure.Services;
using Second.Cojali.Domain.Ports;

namespace Second.Cojali.Infrastructure.Adapters;

public class FileUserRepository : IUserRepository
{
    private readonly string _filePath;

    public FileUserRepository(string filePath)
    {
        _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var users = await ReadFromFileAsync();
        return users ?? Enumerable.Empty<User>();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        var users = await GetAllUsersAsync();
        return users.FirstOrDefault(u => u.Id == id);
    }

    public async Task AddUserAsync(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        var users = (await GetAllUsersAsync()).ToList();

        // Ensure the ID is unique
        user.Id = (users.Count > 0 ? users.Max(u => u.Id) : 0) + 1;
        users.Add(user);

        await SaveToFileAsync(users);
    }

    public async Task UpdateUserAsync(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        var users = (await GetAllUsersAsync()).ToList();
        var existingUser = users.Find(u => u.Id == user.Id);

        if (existingUser == null)
            throw new KeyNotFoundException($"User with ID {user.Id} not found.");

        existingUser.Name = user.Name;
        existingUser.Email = user.Email;

        await SaveToFileAsync(users);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        var users = await GetAllUsersAsync();
        return users.Any(u => u.Id == id);
    }

    private async Task<List<User>?> ReadFromFileAsync()
    {
        try
        {
            if (!File.Exists(_filePath)) return new List<User>();

            var jsonData = await FileService.ReadFileAsync(_filePath);
            return JsonConvert.DeserializeObject<List<User>>(jsonData);
        }
        catch
        {
            // Handle exceptions gracefully, such as file not found or invalid JSON
            return new List<User>();
        }
    }

    private async Task SaveToFileAsync(IEnumerable<User> users)
    {
        try
        {
            var jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
            await FileService.WriteFileAsync(_filePath, jsonData);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to save users to file.", ex);
        }
    }
}
