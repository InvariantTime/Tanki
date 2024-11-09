using Tanki.Domain.Models;

namespace Tanki.Services.Interfaces
{
    public interface IGameStateHandler
    {
        Task OnSessionDisconnect(GameSession session);
    }
}
