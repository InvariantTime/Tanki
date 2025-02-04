using System.Numerics;

namespace Tanki.Game.Objects
{
    public readonly ref struct TankInfo
    {
        public float Rotation { get; init; }

        public float HeadRotation { get; init; }

        public bool CanShoot { get; init; }

        public Vector2 Position { get; init; }

        public Vector2 Velocity { get; init; }
    }
}
