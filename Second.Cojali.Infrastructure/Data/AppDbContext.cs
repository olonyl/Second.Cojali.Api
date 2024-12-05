using Microsoft.EntityFrameworkCore;
using Second.Cojali.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Second.Cojali.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configure entities here if needed
    }
}
