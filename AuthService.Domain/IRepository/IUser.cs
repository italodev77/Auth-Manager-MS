using Auth_ms.Entities;

namespace Auth_ms.Repositories;

public interface IUser
{
    Task<User> GetByEmailAsync(string email);
    Task<User> GetByIdAsync(int userId);
    Task<User> AddAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<User> DeleteAsync(User user);
}