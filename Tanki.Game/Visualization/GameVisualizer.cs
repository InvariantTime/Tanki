using Tanki.Game.Objects;

namespace Tanki.Game.Visualization
{
    public class GameVisualizer
    {
        private readonly List<IVisualObject> _visuals;

        public GameVisualizer()
        {
            _visuals = new();
        }

        public void AddVisual(IVisualObject visual)
        {
            _visuals.Add(visual);
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
