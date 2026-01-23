using Microsoft.AspNetCore.Identity;
using Voxpop.Identity.Api.Middlewares;
using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Application.Options;
using Voxpop.Identity.Application.Services;
using Voxpop.Identity.Application.Services.CodeSender;
using Voxpop.Identity.Application.Services.UserFinder;
using Voxpop.Identity.Domain.Enums;
using Voxpop.Identity.Domain.Interfaces;
using Voxpop.Identity.Domain.Models;
using Voxpop.Identity.Infrastructure.Extensions;
using Voxpop.Identity.Infrastructure.Messaging;
using Voxpop.Identity.Infrastructure.Options;
using Voxpop.Identity.Infrastructure.Persistence;
using Voxpop.Identity.Infrastructure.Persistence.Repositories;
using Voxpop.Identity.Infrastructure.Services;
using Voxpop.Packages.Handler.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.Configure<VerificationCodeOptions>(builder.Configuration.GetSection("VerificationCode"));
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
    .AddScoped<CodeVerifier>()
    .AddScoped<RefreshTokenService>()
    .AddKeyedScoped<ICodeSender, PhoneCodeSender>(VerificationCodeChannel.Phone)
    .AddKeyedScoped<ICodeSender, EmailCodeSender>(VerificationCodeChannel.Email)
    .AddKeyedScoped<IUserFinder, PhoneUserFinder>(VerificationCodeChannel.Phone)
    .AddKeyedScoped<IUserFinder, EmailUserFinder>(VerificationCodeChannel.Email)
    .AddScoped<IPasswordHasher<VerificationCode>, PasswordHasher<VerificationCode>>()
    .AddScoped<IHasher, Hasher>()
    .AddRabbitMq(builder.Configuration.GetSection("RabbitMq").Get<RabbitMqOptions>()!)
    .AddAuthentication(builder.Configuration.GetSection("Jwt").Get<JwtOptions>()!)
    .AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.Run();