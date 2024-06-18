using Microsoft.EntityFrameworkCore;

namespace AccessCodes.Adapters.Sql;

public class AccessCodeContext : DbContext
{
    public AccessCodeContext(DbContextOptions<AccessCodeContext> options) : base(options)
    {
    }

    public DbSet<AccessCode> AccessCodes { get; set; }
}