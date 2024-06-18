using Microsoft.AspNetCore.Mvc.Testing;
using Respawn;
using Testcontainers.MsSql;

namespace AccessCodes.Api.Tests;

public class AccessCodeApi : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _container = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();

    private Respawner? _repawner;

    public async Task InitializeAsync() => await _container.StartAsync();
    public async Task DisposeAsync() => await _container.StopAsync();
    
    public async Task<HttpClient> CreateClient(bool createDb = false)
    {
        var respawner = await GetRespawner();
        await respawner.ResetAsync(_container.GetConnectionString());
        
        var factory = WithWebHostBuilder(builder =>
            builder.UseSetting("ConnectionStrings:AccessCodes", _container.GetConnectionString()));

        return factory.Server.CreateClient();
    }
    
    private async Task<Respawner> GetRespawner()
    {
        _repawner ??= await Respawner.CreateAsync(_container.GetConnectionString());
        return _repawner;
    }
}