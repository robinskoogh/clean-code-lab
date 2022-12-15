using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IScoreKeeper, BullsAndCowsScoreKeeper>();

            return services;
        }
    }
}
