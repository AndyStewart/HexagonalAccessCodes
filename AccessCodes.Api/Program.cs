using AccessCodes;
using AccessCodes.Adapters.Sql;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AccessCodeContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AccessCodes")));

var app = builder.Build();

var isDbCreated = false;
while (!isDbCreated)
{
    try
    {
        await using var scope = app.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AccessCodeContext>();
        isDbCreated = await context.Database.EnsureCreatedAsync();
    }
    catch (Exception e)
    {
        app.Logger.LogError(e, "Error creating database");
    }
    finally
    {
        await Task.Delay(2000);
    }
}

app.Logger.LogInformation("System ready to go"); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", (AccessCodeContext context) => $"Hello World! {context.AccessCodes.Count()}")
    .WithName("GetAccessCodes")
    .WithOpenApi();

app.Run();

public partial class Program { }
