using System.Numerics;

namespace Tanki.Game
{
    public class World
    {
        public float Width { get; }

        public float Height { get; }

        public World(Vector2 size)
        {
            Width = size.X;
            Height = size.Y;
        }

        public Vector2 GetCorrectionalPosition(float radius, Vector2 position)
        {
            float x = GetCorrectionalPositionFor(Width, position.X, radius);
            float y = GetCorrectionalPositionFor(Height, position.Y, radius);

            return new Vector2(x, y);
        }

        private static float GetCorrectionalPositionFor(
            float worldDimension, float objectDimension, float radius)
        {
            if (objectDimension + radius > worldDimension)
            {
                var dif = Math.Abs(objectDimension + radius - worldDimension);
                return objectDimension - dif;
            }

            if (objectDimension - radius < 0)
            {
                var dif = Math.Abs(objectDimension - radius);
                return dif + objectDimension;
            }

            return objectDimension;
        }
    }
}