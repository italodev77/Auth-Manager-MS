using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AuthService.Infrastructure.Data
{
    public class ApiDbContextFactory : IDesignTimeDbContextFactory<ApiDbContext>
    {
        public ApiDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApiDbContext>();

            // Aqui você coloca a connection string que o design time deve usar
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=masterdb;Username=postgres;Password=postgres;");

            return new ApiDbContext(optionsBuilder.Options);
        }
    }
}