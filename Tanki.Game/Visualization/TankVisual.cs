using System.Numerics;

namespace Tanki.Game.Visualization
{
    public class TankVisual : IVisualObject
    {
        public string Object => "tank";

        public Vector2 Position { get; set; }

        public float Rotation { get; set; }

        public float HeadRotation { get; set; }

        public int Health { get; set; }
    }
}
