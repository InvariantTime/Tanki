using System.Timers;
using Tanki.Game.Objects;

namespace Tanki.Game.Collision
{
    public class Collider
    {
        private readonly float _radius;
        private readonly ITransformable _transformable;

        public Collider(ITransformable transformable, float radius)
        {
            _radius = radius;
            _transformable = transformable;
        }

        public bool CollideWith(Collider other)
        {
            var distance = _radius + other._radius;
            var dif = _transformable.Position - other._transformable.Position;

            return dif.Length() <= distance;
        }

        public bool CollideWith(World world)
        {
            var pos = _transformable.Position;

            if (pos.X + _radius >= world.Size.X || pos.X - _radius <= 0)
                return true;

            if (pos.Y + _radius >= world.Size.Y || pos.Y - _radius <= 0)
                return true;

            return false;
        }
    }
}
