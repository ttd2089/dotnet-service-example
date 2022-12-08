using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Things.Api.Configuration;
using Things.Database;
using Things.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IThingsService, ThingsService>();
builder.Services.AddScoped<IDataSource, DataSource>();
builder.Services.AddScoped(typeof(IRepository<,>), typeof(EntityRepository<,>));

var startupConfig = builder.Configuration.GetSection("StartupConfig").Get<StartupConfig>() ?? new();

builder.Services.AddDbContext<ThingsDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddControllers();

if (startupConfig.UseSwagger)
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
}

var app = builder.Build();

if (startupConfig.UseSwagger)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (startupConfig.MigrateDatabase)
{ 
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ThingsDbContext>();
    context.Database.Migrate();
}

app.MapControllers();

app.Run();
