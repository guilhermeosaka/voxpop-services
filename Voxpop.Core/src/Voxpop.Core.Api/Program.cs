using Serilog;
using Voxpop.Core.Api.Middlewares;
using Voxpop.Core.Application.Common.Options;
using Voxpop.Core.Infrastructure.Extensions;
using Voxpop.Core.Infrastructure.Persistence.Common.Migrations;
using Voxpop.Packages.Dispatcher.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseLogger(builder.Configuration);

builder.Services.AddControllers();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDispatcher()
    .AddPersistence(builder.Configuration.GetConnectionString("CoreDb")!)
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