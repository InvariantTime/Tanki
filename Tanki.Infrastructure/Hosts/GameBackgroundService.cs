using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tanki.Services.Interfaces;

namespace Tanki.Infrastructure.Hosts
{
    public class GameBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Game.Game _game;

        public GameBackgroundService(IServiceScopeFactory factory)
        {
            _scopeFactory = factory;
            _game = new Game.Game();
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _scopeFactory.CreateScope();

            var sessions = scope.ServiceProvider.GetRequiredService<ISessionService>();
            var visualizer = scope.ServiceProvider.GetRequiredService<GameServerContext>();

            while (stoppingToken.IsCancellationRequested == false)
            {
                await Task.Delay(10);
                await UpdateSessions(sessions, visualizer);
            }
        }

        private Task UpdateSessions(ISessionService sessionService, GameServerContext visualizer)
        {
            var sessions = sessionService.GetAll();

            var games = sessions.Select(session =>
            {
                var scene = session.Scene.Game;
                var visual = _game.Update(scene, 10);

                return visualizer.Visualize(session.Id, visual);
            });

            return Task.WhenAll(games);
        }
    }
}
