using System.Numerics;
using Tanki.Game.Visualization;

namespace Tanki.Game.Objects
{
    public class Bullet : GameObject
    {
        public const float Radius = 5;

        private const float _speed = 0.3f;

        public Tank Owner { get; }

        public Bullet(Tank owner, Vector2 position, Vector2 direction) : base(Radius)
        {
            Owner = owner;
            Motion.SetPosition(position);
            Motion.SetVelocity(Vector2.Normalize(direction) * _speed);
        }

        protected override void OnUpdate(GameContext context)
        {
        }

        public override void Draw(GameVisualizer visualizer)
        {
            var visual = visualizer.AllocateVisual("bullet");
            visual.InsertObject("position", new VisualPoint(Motion.Position.X, Motion.Position.Y));
        }

        public override void OnCollide(GameObject other)
        {
            if (other == Owner)
                return;

            Destroy();
        }

        public override void OnCollide(World world)
        {
            Destroy();
        }
    }
}
