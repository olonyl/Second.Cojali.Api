using Second.Cojali.Domain.Entities;
using Newtonsoft.Json;
using Second.Cojali.Application.Interfaces;
using Second.Cojali.Infrastructure.Services;
using Second.Cojali.Domain.Ports;

namespace Second.Cojali.Infrastructure.Adapters;

public class UserRepository : IUserRepository
{
    private readonly string _filePath;

    public UserRepository(string filePath)
    {
        _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
    }

    public IEnumerable<User> GetAllUsers() => ReadFromFile() ?? Enumerable.Empty<User>();
    public User? GetById(int id) => GetAllUsers().FirstOrDefault(u => u.Id == id);

    public void AddUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        var users = GetAllUsers().ToList();

        // Ensure the ID is unique
        user.Id = (users.Count > 0 ? users.Max(u => u.Id) : 0) + 1;
        users.Add(user);

        SaveToFile(users);
    }

    public void UpdateUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        var users = GetAllUsers().ToList();
        var existingUser = users.First(u => u.Id == user.Id);

        if (existingUser == null) throw new KeyNotFoundException($"User with ID {user.Id} not found.");

        existingUser.Name = user.Name;
        existingUser.Email = user.Email;

        SaveToFile(users);
    }
    public bool Exists(int id) => GetAllUsers().Any(u => u.Id == id);

    private List<User>? ReadFromFile()
    {
        try
        {
            var jsonData = FileService.ReadFile(_filePath);
            return JsonConvert.DeserializeObject<List<User>>(jsonData);
        }
        catch
        {
            // Handle exceptions gracefully, such as file not found or invalid JSON
            return new List<User>();
        }
    }

    // Private method to handle file writing
    private void SaveToFile(IEnumerable<User> users)
    {
        try
        {
            var jsonData = JsonConvert.SerializeObject(users);
            FileService.WriteFile(_filePath, jsonData);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to save users to file.", ex);
        }
    }
}
