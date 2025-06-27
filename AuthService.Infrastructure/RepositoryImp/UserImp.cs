using Auth_ms.Data;
using Auth_ms.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth_ms.Repositories;

public class UserImp : IUser
{
    private readonly ApiDbContext _context;

    public UserImp(ApiDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.User.FirstOrDefaultAsync(u => u.email == email);
    }
    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.User.FirstOrDefaultAsync(u => u.UserId == id);
    }

    public async Task<User> AddAsync(User user)
    {
        await _context.User.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        var existing = await _context.User.FindAsync(user.UserId);
        if (existing == null)
            return null;
        
        existing.email = user.email;
        existing.hashPassword = user.hashPassword;

        _context.User.Update(existing);
        await _context.SaveChangesAsync();

        return existing;
    }

    public async Task<User> DeleteAsync(User user)
    {
        var existing = await _context.User.FindAsync(user.UserId);
        if (existing == null)
            return null;

        _context.User.Remove(existing);
        await _context.SaveChangesAsync();

        return existing;
    }
}