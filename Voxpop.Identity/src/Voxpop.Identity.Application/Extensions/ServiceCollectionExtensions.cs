using Microsoft.Extensions.DependencyInjection;
using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Application.Services.UserFinders;
using Voxpop.Identity.Domain.Enums;

namespace Voxpop.Identity.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services
            .AddKeyedScoped<IUserFinder, PhoneUserFinder>(VerificationCodeChannel.Phone)
            .AddKeyedScoped<IUserFinder, EmailUserFinder>(VerificationCodeChannel.Email);   
        
        return services;
    }
}