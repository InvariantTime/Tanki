using System.Numerics;

namespace Tanki.Game.Objects
{
    public class Bullet : GameObject
    {
        public const float Radius = 5;

        private const float _speed = 0.5f;

        public Tank Owner { get; }

        private Bullet(Tank owner, Vector2 direction) : base(Radius)
        {
            Owner = owner;
            SetVelocity(Vector2.Normalize(direction) * _speed);
        }

        protected override void OnUpdate()
        {
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

        public static Bullet Create(Tank tank)
        {
            var angle = tank.HeadRotation;
            var direction = new Vector2(MathF.Cos(angle), MathF.Sin(angle));

            return new Bullet(tank, direction);
        }
    }
}
