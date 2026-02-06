using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace inspecto_API.Models;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseNpgsql(
            Environment.GetEnvironmentVariable("SUPABASE_DB_CONNECTION")
            ?? throw new InvalidOperationException("Missing SUPABASE_DB_CONNECTION")
        );

        return new AppDbContext(optionsBuilder.Options);
    }
}
