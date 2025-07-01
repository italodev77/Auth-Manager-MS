
using Auth_ms.Entities;
using AuthService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Auth_ms.Repositories;

public class EnterprisesImp : IEnterprises
{
    private readonly ApiDbContext _context;

    public EnterprisesImp(ApiDbContext context)
    {
        _context = context;
    }

    public async Task<Enterprises> GetByCnpjAsync(string cnpj)
    {
        return await _context.Enterprises.FirstOrDefaultAsync(e => e.cnpj == cnpj);
    }

    public async Task<Enterprises> AddAsync(Enterprises enterprises)
    {
        await _context.Enterprises.AddAsync(enterprises);
        await _context.SaveChangesAsync();
        return enterprises;
    }

    public async Task<Enterprises> UpdateAsync(Enterprises enterprises)
    {
        var existing = await _context.Enterprises.FindAsync(enterprises.EnterpriseId);
        if (existing == null)
            return null;

        existing.EnterpriseName = enterprises.EnterpriseName;
        existing.cnpj = enterprises.cnpj;

        _context.Enterprises.Update(existing);
        await _context.SaveChangesAsync();

        return existing;
    }

    public async Task<Enterprises> DeleteAsync(Enterprises enterprises)
    {
        var existing = await _context.Enterprises.FindAsync(enterprises.EnterpriseId);
        if (existing == null)
            return null;

        _context.Enterprises.Remove(existing);
        await _context.SaveChangesAsync();

        return existing;
    }
}