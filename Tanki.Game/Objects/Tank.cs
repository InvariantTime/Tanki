using System.Numerics;
using Tanki.Game.Controlling;
using Tanki.Game.Visualization;

namespace Tanki.Game.Objects
{
    public class Tank : GameObject
    {
        public const float Radius = 22;
        public const float Speed = 0.1f;
        public const int MaxHealth = 3;

        private readonly ITankController _controller;
        private readonly TankCannon _cannon;

        public float Rotation { get; private set; }

        public int Health { get; private set; }

        public Tank(ITankController controller) : base(Radius)
        {
            Health = MaxHealth;
            _controller = controller;

            _cannon = new TankCannon(this);
        }

        protected override void OnUpdate(GameContext context)
        {
            var info = new TankInfo
            {
                CanShoot = _cannon.CanShoot,
                HeadRotation = _cannon.Rotation,
                Rotation = Rotation,
                Position = Motion.Position,
                Velocity = Motion.Velocity
            };

            var commands = _controller.Update(context.World, info);

            if (commands.Shoot == true)
                _cannon.Shoot(context);

            ChangeMoveState(context, commands.Move);
        }

        public override void Draw(GameVisualizer visualizer)
        {
            var visual = new TankVisual
            {
                HeadRotation = _cannon.Rotation,
                Health = Health,
                Position = Motion.Position,
                Rotation = Rotation
            };

            visualizer.AddVisual(visual);
        }

        public override void OnCollide(GameObject other)
        {
            if (other is Bullet bullet)
            {
                if (bullet.Owner == this)
                    return;

                ApplyDamage();
            }
        }

        public override void OnCollide(World world)
        {
        }

        public void ApplyDamage()
        {
            Health--;
            _controller.AddTankEvent(TankEvents.Attacked);

            if (Health < 0)
            {
                Destroy();
                _controller.AddTankEvent(TankEvents.Destroed);
            }
        }

        private void ChangeMoveState(GameContext context, bool needMove)
        {
            if (needMove == false)
            {
                Motion.Stop();
                return;
            }

            var direction = VectorExtensions.CreateByRotation(Rotation);
            Motion.SetVelocity(direction * Speed);
        }
    }
}