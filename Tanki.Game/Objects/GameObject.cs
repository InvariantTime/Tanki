using System.Numerics;

namespace Tanki.Game.Objects
{
    public abstract class GameObject
    {
        public Vector2 Position { get; private set; }

        public Vector2 Speed { get; private set; }

        public float Rotation { get; private set; }

        public void Update(float dt)
        {
            OnUpdate(dt);
            SetPosition(Position + Speed * dt);
        }

        protected abstract void OnUpdate(float dt);

        protected abstract void OnCollide(GameObject other);

        public void SetPosition(Vector2 position)
        {
            Position = position;
        }
    }
}
