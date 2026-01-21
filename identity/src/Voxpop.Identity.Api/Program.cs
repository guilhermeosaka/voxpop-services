using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Voxpop.Identity.Application.Commands;
using Voxpop.Identity.Application.Handlers;
using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Domain.Interfaces;
using Voxpop.Identity.Domain.Models;
using Voxpop.Identity.Infrastructure.Extensions;
using Voxpop.Identity.Infrastructure.Options;
using Voxpop.Identity.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDb(builder.Configuration.GetConnectionString("IdentityDb"))
    .AddTransient<IHandler<RegisterUserCommand>, RegisterUserHandler>()
    .AddTransient<IUserRepository<User>, UserRepository>()
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
