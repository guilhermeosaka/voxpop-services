using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
using Voxpop.Identity.Application.Options;
using Voxpop.Identity.Infrastructure.Options;
using Voxpop.Identity.Infrastructure.Persistence;
using Voxpop.Identity.Infrastructure.Persistence.Entities;
using Voxpop.Identity.Infrastructure.Persistence.Migrations;

namespace Voxpop.Identity.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddDb(string? connectionString)
        {
            services
                .AddDbContext<IdentityDbContext>(options => options.UseNpgsql(connectionString))
                .AddScoped<DbMigrator>()
                .AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public IServiceCollection AddRabbitMq(RabbitMqOptions options)
        {
            services.AddSingleton<IConnection>(_ =>
            {
                var factory = new ConnectionFactory
                {
                    HostName = options.HostName,
                    UserName = options.UserName,
                    Password = options.Password,
                    VirtualHost = options.VirtualHost,
                    Port = options.Port
                };

                return factory.CreateConnectionAsync().GetAwaiter().GetResult();
            });

            return services;
        }

        public IServiceCollection AddAuthentication(JwtOptions jwtOptions)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudiences = jwtOptions.Audiences,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                    };
                });

            return services;
        }
    }
}