using Tanki.Game.Collision;
using Tanki.Game.Visualization;

namespace Tanki.Game.Objects
{
    public abstract class GameObject
    {
        public bool IsDestroyed { get; private set; }

        public ObjectMotion Motion { get; }

        public Collider Collider { get; }

        public GameObject(float radius)
        {
            Motion = new ObjectMotion();
            Collider = new Collider(Motion, radius);
        }

        public abstract void OnCollide(GameObject other);

        public abstract void OnCollide(World world);
        
        public abstract void Draw(GameVisualizer visualizer);

        protected abstract void OnUpdate(GameContext context);

        public void Update(GameContext context)
        {
            if (IsDestroyed == true)
                return;

            OnUpdate(context);
            Motion.Update(context.DeltaTime);
        }

        public void Destroy()
        {
            IsDestroyed = true;
        }
    }
}