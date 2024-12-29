using System.Numerics;
using Tanki.Game.Collision;

namespace Tanki.Game.Objects
{
    public abstract class GameObject : ITransformable
    {
        public Vector2 Position { get; private set; }

        public Vector2 Velocity { get; private set; }

        public bool IsDestroyed { get; private set; }

        public Collider Collider { get; }

        public GameObject(float radius)
        {
            Collider = new Collider(this, radius);
        }

        public abstract void OnCollide(GameObject other);

        public abstract void OnCollide(World world);

        protected abstract void OnUpdate();

        public void Update(float dt)
        {
            OnUpdate();
            Position += Velocity * dt;
        }

        public void Destroy()
        {
            IsDestroyed = true;
        }

        public void SetPosition(Vector2 position)
        {
            Position = position;
        }

        public void SetVelocity(Vector2 velocity)
        {
            Velocity = velocity;
        }
    }
}