
namespace Tanki.Game.Visualization
{
    public class GameVisualizer
    {
        private readonly List<VisualComposition> _visuals;

        public GameVisualizer()
        {
            _visuals = new();
        }

        public VisualComposition AllocateVisual(string name)
        {
            var visual = new VisualComposition();
            visual.Object = name;

            _visuals.Add(visual);

            return visual;
        }

        public GameVisual Visualize(Scene scene)
        {
            foreach (var obj in scene.Objects)
                obj.Draw(this);

            var visuals = _visuals.ToArray();
            _visuals.Clear();

            return new GameVisual
            {
                Objects = visuals,
                World = scene.World,
            };
        }
    }
}
