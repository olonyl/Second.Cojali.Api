using Microsoft.EntityFrameworkCore;
using Second.Cojali.Domain.Entities;
using Second.Cojali.Domain.Ports;
using Second.Cojali.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Second.Cojali.Infrastructure.Adapters
{
    public class MySqlUserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public MySqlUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null)
                throw new KeyNotFoundException($"User with ID {user.Id} not found.");

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }
    }
}
