using Application;
using Application.Interfaces;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UI_Console;

var host = CreateHostBuilder(args).Build();

var game = host.Services.GetRequiredService<IGame>();

game.RunGame();

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
    .ConfigureServices(
        (_, services) =>
        {
            services.AddApplicationServices();
            services.AddInfrastructureServices();
            services.AddUIServices();
        });
}