using Tanki.Game.Objects;

namespace Tanki.Game
{
    public readonly ref struct GameContext
    {
        public required float DeltaTime { get; init; }

        public required Action<GameObject> Instantiater { get; init; }

        public required IReadOnlyCollection<GameObject> Objects { get; init; }

        public required World World { get; init; }
    }
}
