using Auth_ms.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth_ms.Data;

public class ApiDbContext : DbContext
{
      public ApiDbContext(DbContextOptions options)
            : base(options)
      {
      }

      public DbSet<User> User { get; set; }
      public DbSet<Enterprises> Enterprises { get; set; }
}