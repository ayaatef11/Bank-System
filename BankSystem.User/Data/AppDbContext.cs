using BankSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data;
public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<BankClient> BankClients { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}

