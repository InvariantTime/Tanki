﻿using System.Numerics;

namespace Tanki.Game
{
    public static class VectorExtensions
    {
        private const float _degressOffset = -90;
        private const float _toRadians = MathF.PI / 180;

        public static Vector2 CreateByRotation(float degrees)
        {
            var radians = _toRadians * (degrees + _degressOffset);
            return new Vector2(MathF.Cos(radians), MathF.Sin(radians));
        }
    }
}
