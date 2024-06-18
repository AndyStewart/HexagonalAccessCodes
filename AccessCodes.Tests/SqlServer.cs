using AccessCodes.Adapters.Sql;
using Microsoft.EntityFrameworkCore;
using Respawn;
using Testcontainers.MsSql;

namespace AccessCodes.Tests;

public class SqlServer : IAsyncLifetime
{
    private readonly MsSqlContainer _container = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();

    private Respawner? _repawner;

    public async Task InitializeAsync() => await _container.StartAsync();

    public async Task DisposeAsync() => await _container.StopAsync();

    public async Task<AccessCodeContext> CreateContext(bool createDb = false)
    {
        var respawner = await GetRespawner();
        await respawner.ResetAsync(_container.GetConnectionString());

        var optionsBuilder = new DbContextOptionsBuilder<AccessCodeContext>();
        optionsBuilder.UseSqlServer(_container.GetConnectionString());
        var context = new AccessCodeContext(optionsBuilder.Options);

        if (createDb)
            await context.Database.EnsureCreatedAsync();
        
        return context;
    }

    private async Task<Respawner> GetRespawner()
    {
        _repawner ??= await Respawner.CreateAsync(_container.GetConnectionString());
        return _repawner;
    }
}