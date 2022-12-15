using Application;
using Application.Helpers;
using Application.Interfaces;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UI_Console;

var host = CreateHostBuilder(args).Build();

IConfiguration configuration = host.Services.GetRequiredService<IConfiguration>();

var ioHelper = host.Services.GetRequiredService<IIOHelper>();
var scoreKeeper = host.Services.GetRequiredService<IScoreKeeper>();
var game = host.Services.GetRequiredService<IGame>();

game.SelectGame();

//game.SetGameEngine();

game.RunGame();

//gameEngine.StartGameEngine();


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
};