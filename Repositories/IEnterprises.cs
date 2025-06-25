using Auth_ms.Entities;

namespace Auth_ms.Repositories;

public interface IEnterprises
{
    Task<Enterprises> GetByCnpjAsync(string cnpj);
    Task<Enterprises> AddAsync(Enterprises enterprises);
    Task<Enterprises> UpdateAsync(Enterprises enterprises);
    Task<Enterprises> DeleteAsync(Enterprises enterprises);
}