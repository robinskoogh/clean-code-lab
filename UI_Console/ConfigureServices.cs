using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace UI_Console
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddUIServices(this IServiceCollection services)
        {
            services.AddSingleton<IGame, Game>();

            return services;
        }
    }
}
