using Tanki.Game.Objects;

namespace Tanki.Game.Collision
{
    public class Collider
    {
        private readonly float _radius;
        private readonly ObjectMotion _motion;

        public Collider(ObjectMotion motion, float radius)
        {
            _radius = radius;
            _motion = motion;
        }

        public bool CollideWith(Collider other)
        {
            var distance = _radius + other._radius;
            var dif = _motion.Position - other._motion.Position;

            return dif.Length() < distance;
        }

        public bool CollideWith(World world)
        {
            var pos = _motion.Position;

            if (pos.X + _radius > world.Width || pos.X - _radius < 0)
                return true;

            if (pos.Y + _radius > world.Height || pos.Y - _radius < 0)
                return true;

            return false;
        }
    }
}
