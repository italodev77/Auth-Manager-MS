using Auth_ms.Entities;

namespace Auth_ms.Repositories;

public interface IEnterprises
{
    Task<IEnterprises?> GetByCnpjAsync(string cnpj);
    Task AddAsync(Enterprises enterprises);
    Task UpdateAsync(Enterprises enterprises);
    Task DeleteAsync(Enterprises enterprises);
}