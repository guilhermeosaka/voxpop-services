using Serilog;
using Voxpop.Packages.Dispatcher.Extensions;
using Voxpop.Profile.Api.Middlewares;
using Voxpop.Profile.Application.Options;
using Voxpop.Profile.Infrastructure.Extensions;
using Voxpop.Profile.Infrastructure.Persistence.Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseLogger(builder.Configuration);

builder.Services.AddControllers();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDispatcher()
    .AddPersistence(builder.Configuration.GetConnectionString("ProfileDb")!)
    .AddAuth(builder.Configuration.GetSection("Jwt").Get<JwtOptions>()!)
    .AddInfrastructureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<Migrator>();
    await dbInitializer.MigrateAsync();
}

app
    .UseSerilogRequestLogging()
    .UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization()
    .UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();