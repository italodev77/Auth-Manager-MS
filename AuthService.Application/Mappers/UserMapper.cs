using Auth_ms.Dtos;
using Auth_ms.Entities;

namespace Auth_ms.Mappers;

public static class UserMapper
{
    public static User ToEntity(RegisterDto dto)
    {
        return new User
        {
            
            email = dto.Email,
            hashPassword = dto.Password,
        };
    }

    public static User ToEntity(UpdateUserDto dto)
    {
        return new User
        {
            email = dto.Email,
            hashPassword = dto.Password,
        };
    }

    public static UserResponseDto ToResponseDto(User entity)
    {
        return new UserResponseDto
        {
            Email = entity.email,
            
        };
    }
}