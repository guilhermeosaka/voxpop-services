using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Voxpop.Profile.Application.Interfaces;
using Voxpop.Profile.Application.Options;
using Voxpop.Profile.Domain.Interfaces;
using Voxpop.Profile.Infrastructure.Persistence;

using Voxpop.Profile.Infrastructure.Persistence.Repositories;
using Voxpop.Profile.Infrastructure.Persistence.Seed;

namespace Voxpop.Profile.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, string? connectionString)
    {
        services
            .AddDbContext<ProfileDbContext>(options => options.UseNpgsql(connectionString))
            .AddScoped<IProfileRepository, ProfileRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<Seeder>();

        return services;
    }
    
    public static IServiceCollection AddAuth(this IServiceCollection services, JwtOptions jwtOptions)
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

        services.AddAuthorization();

        return services;
    }
}