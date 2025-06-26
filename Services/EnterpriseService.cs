using Auth_ms.Entities;
using Auth_ms.Repositories;

namespace Auth_ms.Services;

public class EnterpriseService
{
    private readonly IEnterprises _enterpriseRepository;

    public EnterpriseService(IEnterprises enterpriseRepository)
    {
        _enterpriseRepository = enterpriseRepository;
    }

    public async Task<Enterprises> CreateEnterpriseAsync(Enterprises enterprise)
    {
        var validation = EnterpriseValidator.ValidateCreate(enterprise);
        if (!validation.IsValid)
            throw new ArgumentException(string.Join(" | ", validation.Errors));

        var existing = await _enterpriseRepository.GetByCnpjAsync(enterprise.cnpj);
        if (existing is not null)
            throw new InvalidOperationException("Já existe uma empresa cadastrada com esse CNPJ.");

        return await _enterpriseRepository.AddAsync(enterprise);
    }

    public async Task<Enterprises> GetByCnpjAsync(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            throw new ArgumentException("CNPJ é obrigatório.");

        return await _enterpriseRepository.GetByCnpjAsync(cnpj);
    }

    public async Task<Enterprises> UpdateEnterpriseAsync(Enterprises enterprise)
    {
        var validation = EnterpriseValidator.ValidateUpdateOrDelete(enterprise);
        if (!validation.IsValid)
            throw new ArgumentException(string.Join(" | ", validation.Errors));

        var existing = await _enterpriseRepository.GetByCnpjAsync(enterprise.cnpj);
        if (existing is null)
            throw new InvalidOperationException("Empresa não encontrada para atualização.");

        return await _enterpriseRepository.UpdateAsync(enterprise);
    }

    public async Task<Enterprises> DeleteEnterpriseAsync(Enterprises enterprise)
    {
        var validation = EnterpriseValidator.ValidateUpdateOrDelete(enterprise);
        if (!validation.IsValid)
            throw new ArgumentException(string.Join(" | ", validation.Errors));

        var existing = await _enterpriseRepository.GetByCnpjAsync(enterprise.cnpj);
        if (existing is null)
            throw new InvalidOperationException("Empresa não encontrada para exclusão.");

        return await _enterpriseRepository.DeleteAsync(existing);
    }
}
