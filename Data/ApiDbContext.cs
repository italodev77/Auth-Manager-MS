using Microsoft.EntityFrameworkCore;

namespace Auth_ms.Data;

public class ApiDbContext: DbContext
{
      public ApiDbContext(DbContextOptions options)
            : base(options)
      {
      }
}