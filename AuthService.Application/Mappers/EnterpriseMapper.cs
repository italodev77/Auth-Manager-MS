using Auth_ms.Dtos;
using Auth_ms.Entities;

namespace Auth_ms.Mappers;

public static class EnterpriseMapper
{
    public static Enterprises ToEntity(CreateEnterpriseDto dto)
    {
        return new Enterprises
        {
            EnterpriseName = dto.EnterpriseName,
            cnpj = dto.Cnpj,
            EnterpriseEmail = dto.EnterpriseEmail,
            DbPath = dto.DbPath,
            DbUsername = dto.DbUsername,
            EnterpriseDbPassword = dto.EnterpriseDbPassword,
            Status = dto.Status
        };
    }

    public static Enterprises ToEntity(UpdateEnterpriseDto dto)
    {
        return new Enterprises
        {
            EnterpriseId = dto.EnterpriseId,
            EnterpriseName = dto.EnterpriseName,
            cnpj = dto.Cnpj,
            EnterpriseEmail = dto.EnterpriseEmail,
            DbPath = dto.DbPath,
            DbUsername = dto.DbUsername,
            EnterpriseDbPassword = dto.EnterpriseDbPassword,
            Status = dto.Status
        };
    }

    public static EnterpriseResponseDto ToResponseDto(Enterprises entity)
    {
        return new EnterpriseResponseDto
        {
            EnterpriseId = entity.EnterpriseId,
            EnterpriseName = entity.EnterpriseName,
            Cnpj = entity.cnpj,
            EnterpriseEmail = entity.EnterpriseEmail,
            DbPath = entity.DbPath,
            DbUsername = entity.DbUsername,
            Status = entity.Status
        };
    }
}