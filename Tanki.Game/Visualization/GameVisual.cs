namespace Tanki.Game.Visualization
{
    public record GameVisual
    {
        public required IEnumerable<VisualComposition> Objects { get; init; }

        public required World World { get; init; }
    }
}
