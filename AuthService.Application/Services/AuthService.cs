using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Auth_ms.Data;
using Auth_ms.Dtos;
using Auth_ms.Entities;
using Auth_ms.Config;

namespace Auth_ms.Services;

public class AuthService
{
    private readonly ApiDbContext _context;
    private readonly TokenService _tokenService;
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthService(ApiDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<TokenResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _context.User.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null)
            throw new UnauthorizedAccessException("Usuário não encontrado.");

        var result = _passwordHasher.VerifyHashedPassword(user, user.hashPassword, dto.Password);
        if (result == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException("Senha incorreta.");

        return _tokenService.GenerateToken(user);
    }

    public async Task RegisterAsync(RegisterDto dto)
    {
        if (await _context.User.AnyAsync(u => u.Email == dto.Email))
            throw new InvalidOperationException("E-mail já está em uso.");

        if (string.IsNullOrWhiteSpace(dto.Password))
            throw new ArgumentException("Senha não pode ser vazia.");

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Role = "user",
            Status = "active",
            hashPassword = _passwordHasher.HashPassword(null, dto.Password)
        };

        _context.User.Add(user);
        await _context.SaveChangesAsync();
    }
}