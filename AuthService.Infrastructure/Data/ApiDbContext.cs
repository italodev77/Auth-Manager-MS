using Auth_ms.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) {}

        public DbSet<User> User { get; set; }
        public DbSet<Enterprises> Enterprises { get; set; }
    }
}