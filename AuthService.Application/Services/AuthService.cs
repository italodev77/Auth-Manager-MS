using Auth_ms.Application.Contracts;
using Auth_ms.Dtos;
using Auth_ms.Entities;
using Auth_ms.Mappers;
using Auth_ms.Repositories;
using Auth_ms.Validations;
using AuthService.Application.Contracts;

namespace Auth_ms.Services;

public class AuthService : IAuthService
{
    private readonly IAuthService _authRepository;
    private readonly TokenService _tokenService;
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthService(IAuth authRepository, ITokenService tokenService)
    {
        _authRepository = authRepository;
        _tokenService = tokenService;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task RegisterAsync(RegisterDto dto)
    {
        var entity = UserMapper.ToEntity(dto);
        var validation = UserValidator.ValidateCreate(entity);

        if (!validation.IsValid)
            throw new ArgumentException(string.Join("; ", validation.Errors));

        var existing = await _authRepository.GetByEmailAsync(dto.Email);
        if (existing != null)
            throw new InvalidOperationException("E-mail já cadastrado.");

        entity.hashPassword = _passwordHasher.HashPassword(null, dto.Password);
        await _authRepository.AddAsync(entity);
    }

    public async Task<TokenResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _authRepository.GetByEmailAsync(dto.Email);
        if (user == null)
            throw new UnauthorizedAccessException("Usuário não encontrado.");

        var result = _passwordHasher.VerifyHashedPassword(user, user.hashPassword, dto.Password);
        if (result == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException("Senha incorreta.");

        return _tokenService.GenerateToken(user);
    }
}