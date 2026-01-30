using Microsoft.Extensions.DependencyInjection;
using Voxpop.Core.Application.Votes.UseCases.SubmitVote.Strategies;
using Voxpop.Core.Domain.Polls.Enums;

namespace Voxpop.Core.Application.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services
            .AddKeyedScoped<ISubmitVoteStrategy, SingleChoiceStrategy>(VoteMode.SingleChoice)
            .AddKeyedScoped<ISubmitVoteStrategy, MultipleChoiceStrategy>(VoteMode.MultipleChoice);

        return services;
    }
}