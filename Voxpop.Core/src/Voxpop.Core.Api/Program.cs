using Serilog;
using Voxpop.Core.Api.Middlewares;
using Voxpop.Core.Application.Common.Extensions;
using Voxpop.Core.Application.Common.Options;
using Voxpop.Core.Infrastructure.Extensions;
using Voxpop.Core.Infrastructure.Persistence.Common.Migrations;
using Voxpop.Packages.Dispatcher.Extensions;
using Voxpop.Packages.Extensions;

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
    .AddApplicationServices()
    .AddInfrastructureServices()
    .AddHealthChecks();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("DevCors");
}

app.MapHealthChecks("/health");

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