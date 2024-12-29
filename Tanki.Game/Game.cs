using Tanki.Game.Collision;

namespace Tanki.Game
{
    public class Game
    {
        private readonly CollisionResolver _collisions;

        public Game()
        {
            _collisions = new CollisionResolver();
        }

        public GameVisual Update(Scene scene)
        {
            foreach (var obj in scene.Objects)
                obj.Update(1);

            _collisions.Resolve(scene.World, scene.Objects);

            scene.UpdateState();

            return new GameVisual
            {
                Objects = scene.Objects,
                World = scene.World
            };
        }
    }
}
