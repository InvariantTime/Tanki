using System.Numerics;

namespace Tanki.Game.Objects
{
    public class Bullet : GameObject//TODO: Use object pool pattern
    {
        private const int _damage = 15;
        private const float _speed = 5;

        public Bullet(Vector2 direction)
        {
            Speed = Vector2.Normalize(direction) * _speed;
        }

        protected override void OnUpdate(float dt)
        {
        }

        protected override void OnCollide(GameObject other)
        {
            if (other is Tank tank)
            {
                tank.TakeDamage(_damage);
            }
            else if (other is Bullet)
            {
                other.Destroy();
            }

            Destroy();
        }
    }
}
