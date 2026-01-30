using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Voxpop.Core.Application.Common.Interfaces;
using Voxpop.Core.Application.Common.Options;
using Voxpop.Core.Application.Polls.Queries;
using Voxpop.Core.Application.Profiles.Queries;
using Voxpop.Core.Domain.Common.Interfaces;
using Voxpop.Core.Domain.Profiles.Repositories;
using Voxpop.Core.Domain.Reactions.Repositories;
using Voxpop.Core.Domain.Votes.Repositories;
using Voxpop.Core.Infrastructure.Identity;
using Voxpop.Core.Infrastructure.Persistence.Common;
using Voxpop.Core.Infrastructure.Persistence.Common.Dapper;
using Voxpop.Core.Infrastructure.Persistence.Common.Interceptors;
using Voxpop.Core.Infrastructure.Persistence.Common.Migrations;
using Voxpop.Core.Infrastructure.Persistence.Common.Repositories;
using Voxpop.Core.Infrastructure.Persistence.Polls.Queries;
using Voxpop.Core.Infrastructure.Persistence.Profiles.Queries;
using Voxpop.Core.Infrastructure.Persistence.Profiles.Repositories;
using Voxpop.Core.Infrastructure.Persistence.Reactions.Repositories;
using Voxpop.Core.Infrastructure.Persistence.Votes.Repositories;
using Voxpop.Core.Infrastructure.Services;

namespace Voxpop.Core.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
    {
        services
            .AddDbContext<CoreDbContext>(options => options.UseNpgsql(connectionString))
            .AddScoped(typeof(IRepository<>), typeof(Repository<>))
            .AddScoped<IProfileRepository, ProfileRepository>()
            .AddScoped<IVoteRepository, VoteRepository>()
            .AddScoped<IReactionRepository, ReactionRepository>()
            .AddScoped<IProfileQueries, ProfileQueries>()
            .AddScoped<IPollQueries, PollQueries>()
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