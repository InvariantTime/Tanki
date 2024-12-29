using Tanki.Game.Utils;

namespace Tanki.Game.Objects
{
    public class Tank : GameObject
    {
        private const float _shootCooldown = 1;

        public const float Radius = 22;

        public const int MaxHealth = 3;

        private readonly GameTimer _shootTimer;

        public float Rotation { get; private set; }

        public float HeadRotation { get; private set; }

        public int Health { get; private set; }

        public Tank() : base(Radius)
        {
            _shootTimer = new GameTimer(_shootCooldown);
            Health = MaxHealth;
        }

        protected override void OnUpdate()
        {
            HeadRotation += 0.04f;
            Shoot();
        }

        public override void OnCollide(GameObject other)
        {
            if (other is Bullet bullet)
            {
              //  if (bullet.Owner == this)
                 //   return;

              //  ApplyDamage();
            }
        }

        public override void OnCollide(World world)
        {
        }

        public void ApplyDamage()
        {
            Health--;

            if (Health < 0)
                Destroy();
        }

        private void Shoot()
        {

        }
    }
}
