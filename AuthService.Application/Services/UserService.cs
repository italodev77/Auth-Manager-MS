using Auth_ms.Dtos;
using Auth_ms.Mappers;
using Auth_ms.Repositories;
using Auth_ms.Entities;
using Auth_ms.Validations;

namespace Auth_ms.Services;

public class UserService
{
    private readonly IUser _userRepository;

    public UserService(IUser userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponseDto> CreateUserAsync(RegisterDto dto)
    {
        var entity = UserMapper.ToEntity(dto);
        var validation = UserValidator.ValidateCreate(entity);

        if (!validation.IsValid)
            throw new ArgumentException(string.Join("; ", validation.Errors));

        var existing = await _userRepository.GetByEmailAsync(dto.Email);
        if (existing != null)
            throw new InvalidOperationException("E-mail já cadastrado.");

        var created = await _userRepository.AddAsync(entity);
        return UserMapper.ToResponseDto(created);
    }

    public async Task<UserResponseDto> UpdateUserAsync(UpdateUserDto dto)
    {
        var entity = UserMapper.ToEntity(dto);
        var validation = UserValidator.ValidateUpdateOrDelete(entity);

        if (!validation.IsValid)
            throw new ArgumentException(string.Join("; ", validation.Errors));

        var existing = await _userRepository.GetByIdAsync(dto.id);
        if (existing == null)
            throw new InvalidOperationException("Usuário não encontrado.");

        var updated = await _userRepository.UpdateAsync(entity);
        return UserMapper.ToResponseDto(updated);
    }

    public async Task<UserResponseDto> DeleteUserAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("ID inválido.");

        var existing = await _userRepository.GetByIdAsync(id);
        if (existing == null)
            throw new InvalidOperationException("Usuário não encontrado.");

        var deleted = await _userRepository.DeleteAsync(existing);
        return UserMapper.ToResponseDto(deleted);
    }

    public async Task<UserResponseDto?> GetByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        return user == null ? null : UserMapper.ToResponseDto(user);
    }
}
