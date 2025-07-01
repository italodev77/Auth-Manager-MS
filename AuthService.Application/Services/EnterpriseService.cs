using Auth_ms.Dtos;
using Auth_ms.Mappers;
using Auth_ms.Repositories;
using Auth_ms.Entities;
using AuthService.Application.validations;
using AuthService.Domain.shared;

namespace Auth_ms.Services;

public class EnterpriseService
{
    private readonly IEnterprises _enterpriseRepository;

    public EnterpriseService(IEnterprises enterpriseRepository)
    {
        _enterpriseRepository = enterpriseRepository;
    }

    public async Task<EnterpriseResponseDto> CreateEnterpriseAsync(CreateEnterpriseDto dto)
    {
        var existing = await _enterpriseRepository.GetByCnpjAsync(dto.Cnpj);
        if (existing != null)
            throw new InvalidOperationException("CNPJ já cadastrado.");

        var entity = EnterpriseMapper.ToEntity(dto);
        var created = await _enterpriseRepository.AddAsync(entity);
        return EnterpriseMapper.ToResponseDto(created);
    }

    public async Task<EnterpriseResponseDto?> GetByCnpjAsync(string cnpj)
    {
        var result = await _enterpriseRepository.GetByCnpjAsync(cnpj);
        return result == null ? null : EnterpriseMapper.ToResponseDto(result);
    }

    public async Task<Result<EnterpriseResponseDto>> UpdateEnterpriseAsync(UpdateEnterpriseDto dto)
    {
        if (!CnpjValidator.IsValid(dto.Cnpj))
            return Result<EnterpriseResponseDto>.Fail("CNPJ inválido.");

        var existing = await _enterpriseRepository.GetByCnpjAsync(dto.Cnpj);
        if (existing == null)
            return Result<EnterpriseResponseDto>.Fail("Empresa não encontrada.");

        existing.Update(
            enterpriseName: dto.EnterpriseName,
            enterpriseEmail: dto.EnterpriseEmail,
            dbPath: dto.DbPath,
            dbUsername: dto.DbUsername,
            enterpriseDbPassword: dto.EnterpriseDbPassword,
            status: dto.Status
        );

        var updated = await _enterpriseRepository.UpdateAsync(existing);
        return Result<EnterpriseResponseDto>.Ok(EnterpriseMapper.ToResponseDto(updated));
    }


    public async Task<EnterpriseResponseDto> DeleteByCnpjAsync(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            throw new ArgumentException("CNPJ inválido.");

        var existing = await _enterpriseRepository.GetByCnpjAsync(cnpj);
        if (existing == null)
            throw new InvalidOperationException("Empresa não encontrada.");

        var deleted = await _enterpriseRepository.DeleteAsync(existing);
        return EnterpriseMapper.ToResponseDto(deleted);
    }
}
