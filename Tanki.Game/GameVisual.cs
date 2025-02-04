using Tanki.Game.Visualization;

namespace Tanki.Game
{
    public readonly struct GameVisual
    {
        public required IEnumerable<IVisualObject> Objects { get; init; }

        public required World World { get; init; }
    }
}
