using Microsoft.AspNetCore.Identity;
using Voxpop.Identity.Api.Middlewares;
using Voxpop.Identity.Application.Extensions;
using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Application.Options;
using Voxpop.Identity.Application.Services;
using Voxpop.Identity.Domain.Interfaces;
using Voxpop.Identity.Domain.Models;
using Voxpop.Identity.Infrastructure.Extensions;
using Voxpop.Identity.Infrastructure.Messaging;
using Voxpop.Identity.Infrastructure.Options;
using Voxpop.Identity.Infrastructure.Persistence;
using Voxpop.Identity.Infrastructure.Persistence.Migrations;
using Voxpop.Identity.Infrastructure.Persistence.Repositories;
using Voxpop.Identity.Infrastructure.Services;
using Voxpop.Packages.Dispatcher.Extensions;
using Voxpop.Packages.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseLogger(builder.Configuration);

builder.Services.AddControllers();
builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.Configure<VerificationCodeOptions>(builder.Configuration.GetSection("VerificationCode"));
builder.Services.Configure<TwilioOptions>(builder.Configuration.GetSection("Twilio"));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<RefreshTokenOptions>(builder.Configuration.GetSection("RefreshToken"));

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDispatcher()
    .AddDb(builder.Configuration.GetConnectionString("IdentityDb"))
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IVerificationCodeRepository, VerificationCodeRepository>()
    .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddScoped<IMessagePublisher, RabbitMqPublisher>()
    .AddScoped<ITokenGenerator, JwtGenerator>()
    .AddScoped<RefreshTokenService>()
    .AddScoped<IPasswordHasher<VerificationCode>, PasswordHasher<VerificationCode>>()
    .AddScoped<IHasher, Hasher>()
    .AddRabbitMq(builder.Configuration.GetSection("RabbitMq").Get<RabbitMqOptions>()!)
    .AddTwilio(builder.Configuration.GetSection("Twilio").Get<TwilioOptions>()!)
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Environment)
    .AddAuthentication(builder.Configuration.GetSection("Jwt").Get<JwtOptions>()!)
    .AddAuthorization()
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
    var migrator = scope.ServiceProvider.GetRequiredService<DbMigrator>();
    await migrator.MigrateAsync();
}

if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.Run();