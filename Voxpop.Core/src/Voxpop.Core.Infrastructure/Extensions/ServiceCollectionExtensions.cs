using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Voxpop.Core.Application.Interfaces;
using Voxpop.Core.Application.Options;
using Voxpop.Core.Domain.Interfaces;
using Voxpop.Core.Infrastructure.Identity;
using Voxpop.Core.Infrastructure.Persistence;
using Voxpop.Core.Infrastructure.Persistence.Dapper;
using Voxpop.Core.Infrastructure.Persistence.Interceptors;
using Voxpop.Core.Infrastructure.Persistence.Migrations;
using Voxpop.Core.Infrastructure.Persistence.Queries;
using Voxpop.Core.Infrastructure.Persistence.Repositories;
using Voxpop.Core.Infrastructure.Services;

namespace Voxpop.Core.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
    {
        services
            .AddDbContext<CoreDbContext>(options => options.UseNpgsql(connectionString))
            .AddScoped<IUserProfileRepository, UserProfileRepository>()
            .AddScoped<IUserProfileQueries, UserProfileQueries>()
            .AddScoped(typeof(IRepository<>), typeof(Repository<>))
            .AddScoped<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString))
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<Migrator>()
            .AddScoped<AuditSaveChangesInterceptor>();

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
        
        services.AddHttpContextAccessor();
        services.AddScoped<IRequestContext, HttpRequestContext>();

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IClock, SystemClock>();
        
        return services;
    }
}