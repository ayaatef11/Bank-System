using BankSystem.Data;
using BankSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Services;

public class UserService : IUserService
{
    private readonly BankDbContext _context;

    public UserService(BankDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> FindAsync(string userName)
    {
        return await _context.Users.FindAsync(userName);
    }

    public async Task<User?> FindAsync(string userName, string password)
    {
        return await _context.Users.FirstOrDefaultAsync(u =>
            u.UserName == userName && u.Password == password);
    }

    public async Task<bool> AddAsync(User user)
    {
        if (await _context.Users.AnyAsync(u => u.UserName == user.UserName))
            return false;

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(User user)
    {
        var existing = await _context.Users.FindAsync(user.UserName);
        if (existing == null) return false;

        existing.Password = user.Password;
        existing.Permissions = user.Permissions;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(string userName)
    {
        var user = await _context.Users.FindAsync(userName);
        if (user == null) return false;

        user.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
}
