using System.Numerics;

namespace Tanki.Game.Objects
{
    public abstract class GameObject : IPositionProvider
    {
        public Vector2 Position { get; private set; }

        public Vector2 Speed { get; protected set; }

        public float Rotation { get; protected set; }

        public bool NeedDestroy { get; private set; }

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

        public void Destroy()
        {
            NeedDestroy = true;
        }
    }
}
