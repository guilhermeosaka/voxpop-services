using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Application.Options;
using Voxpop.Identity.Application.Services;
using Voxpop.Identity.Domain.Enums;
using Voxpop.Identity.Domain.Interfaces;
using Voxpop.Identity.Domain.Models;
using Voxpop.Identity.Infrastructure.Extensions;
using Voxpop.Identity.Infrastructure.Messaging;
using Voxpop.Identity.Infrastructure.Options;
using Voxpop.Identity.Infrastructure.Persistence;
using Voxpop.Identity.Infrastructure.Persistence.Repositories;
using Voxpop.Packages.Handler.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.Configure<VerificationCodeOptions>(builder.Configuration.GetSection("VerificationCode"));

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDispatcher()
    .AddDb(builder.Configuration.GetConnectionString("IdentityDb"))
    .AddScoped<IUserRepository<User>, UserRepository>()
    .AddScoped<IVerificationCodeRepository, VerificationCodeRepository>()
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddScoped<IMessagePublisher, RabbitMqPublisher>()
    .AddKeyedScoped<ICodeSender, PhoneCodeSender>(VerificationCodeChannel.Phone)
    .AddKeyedScoped<ICodeSender, EmailCodeSender>(VerificationCodeChannel.Email)
    .AddRabbitMq(builder.Configuration.GetSection("RabbitMq").Get<RabbitMqOptions>()!);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();