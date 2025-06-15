using BankSystem.Entities;

namespace BankSystem.Services.Interfaces;
public interface IUsersService
{
    Task<List<User>> GetAllAsync();
    Task<User?> FindAsync(string userName);
    Task<User?> FindAsync(string userName, string password);
    Task<bool> AddAsync(User user);
    Task<bool> UpdateAsync(User user);
    Task<bool> DeleteAsync(string userName);
}

