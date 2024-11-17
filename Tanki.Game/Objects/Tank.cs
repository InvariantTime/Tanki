
namespace Tanki.Game.Objects
{
    public class Tank : GameObject
    {
        private readonly ITankController _controller;

        public int Health { get; private set; }

        public bool IsDead => Health <= 0;

        public Tank(ITankController controller)
        {
            _controller = controller;
        }

        protected override void OnUpdate(float dt)
        {
            _controller.Controll(this, dt);
        }

        protected override void OnCollide(GameObject other)
        {
        }

        public void TakeDamage(int value)
        {
            if (value <= 0)
                return;

            Health -= value;
        }
    }
}