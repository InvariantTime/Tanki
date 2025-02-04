using System.Numerics;

namespace Tanki.Game.Visualization
{
    public class BulletVisual : IVisualObject
    {
        public string Object => "bullet";

        public Vector2 Position { get; set; }
    }
}
