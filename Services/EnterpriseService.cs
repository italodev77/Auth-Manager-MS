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
        return await _enterpriseRepository.AddAsync(enterprise);
    }

    public async Task<Enterprises> GetByCnpjAsync(string cnpj)
    {
        return await _enterpriseRepository.GetByCnpjAsync(cnpj);
    }

    public async Task<Enterprises> UpdateEnterpriseAsync(Enterprises enterprise)
    {
        return await _enterpriseRepository.UpdateAsync(enterprise);
    }

    public async Task<Enterprises> DeleteEnterpriseAsync(Enterprises enterprise)
    {
        return await _enterpriseRepository.DeleteAsync(enterprise);
    }
}