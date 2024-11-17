using System.Numerics;

namespace Tanki.Game.Objects
{
    public class Bullet : GameObject
    {
        private const float _speed = 5;

        public Vector2 Direction { get; }

        public Bullet(Vector2 direction)
        {
            Direction = Vector2.Normalize(direction);
        }

        public override void Update(float dt)
        {
            var delta = Direction * dt * _speed;
            SetPosition(Position + delta);
        }

        protected override void OnCollide(GameObject other)
        {

        }
    }
}
