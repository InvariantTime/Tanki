using Tanki.Game.Collision;
using Tanki.Game.Visualization;

namespace Tanki.Game
{
    public class Game
    {
        private readonly CollisionResolver _collisions;
        private readonly GameVisualizer _visualizer;

        public Game()
        {
            _collisions = new CollisionResolver();
            _visualizer = new GameVisualizer();
        }

        public GameVisual Update(Scene scene, float deltaTime)
        {
            var context = new GameContext
            {
                DeltaTime = deltaTime,
                Instantiater = scene.AddObject,
                Objects = scene.Objects,
                World = scene.World,
            };

            foreach (var obj in scene.Objects)
                obj.Update(context);

            _collisions.Resolve(scene.World, scene.Objects);

            scene.UpdateState();

            var visual = _visualizer.Visualize(scene);
            return visual;
        }
    }
}
