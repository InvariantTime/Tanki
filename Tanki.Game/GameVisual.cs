using Tanki.Game.Objects;

namespace Tanki.Game
{
    public readonly struct GameVisual
    {
        public required IReadOnlyCollection<GameObject> Objects { get; init; }

        public required World World { get; init; }
    }
}
