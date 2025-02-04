using System.Numerics;
using Tanki.Game.Utils;

namespace Tanki.Game.Objects
{
    public class TankCannon
    {
        private const float _shootCooldown = 1;

        private readonly GameTimer _shootTimer;
        private readonly Tank _tank;

        public float Rotation { get; private set; }

        public bool CanShoot => _shootTimer.IsTimeOut == true;

        public TankCannon(Tank tank)
        {
            _shootTimer = new GameTimer(_shootCooldown);
            _tank = tank;
        }

        public void Shoot(GameContext context)
        {
            if (CanShoot == false)
                return;

            var direction = VectorExtensions.CreateByRotation(Rotation);

            var bullet = new Bullet(_tank, _tank.Motion.Position, direction);
            context.Instantiater.Invoke(bullet);

            _shootTimer.Reset();
        }

        public void Rotate(float angle)
        {
            Rotation = angle;
        }
    }
}
