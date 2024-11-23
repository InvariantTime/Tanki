using System.Numerics;

namespace Tanki.Game.Objects
{
    public class Collider
    {
        public float Radius { get; }

        public IPositionProvider Provider { get; }

        public Collider(float radius, IPositionProvider provider)
        {
            Radius = radius;
            Provider = provider;
        }

        public bool CollideWithOther(Collider other)
        {
            var maxDistance = Radius + other.Radius;

            var pos1 = Provider.Position;
            var pos2 = other.Provider.Position;

            return Vector2.Distance(pos1, pos2) <= maxDistance;
        }

        public bool CollideWithWorld(World world)
        {
            var pos = Provider.Position;

            if (pos.X - Radius <= 0 || pos.Y - Radius <= 0)
                return true;

            if (pos.X + Radius >= world.Size.X || pos.Y + Radius >= world.Size.Y)
                return true;

            return false;
        }
    }
}
