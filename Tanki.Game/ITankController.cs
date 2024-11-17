using Tanki.Game.Objects;

namespace Tanki.Game
{
    public interface ITankController
    {
        void Controll(Tank tank, float dt);

        void PushEvent();//TODO: event data
    }
}
