using System.Numerics;

namespace Tanki.Game.Objects
{
    public class ObjectMotion
    {
        public Vector2 Position { get; private set; }

        public Vector2 Velocity { get; private set; }

        public void Update(float dt)
        {
            Position += Velocity * dt;
        }

        public void SetPosition(Vector2 position)
        {
            Position = position;
        }

        public void SetVelocity(Vector2 velocity)
        {
            Velocity = velocity;
        }

        public void Stop()
        {
            Velocity = Vector2.Zero;
        }
    }
}
