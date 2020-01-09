using System;
using System.Numerics;

namespace UnnamedProject.Render.Software
{
    public static class MathHelper
    {
        public static float GetGradient(int y, Vector3 p1, Vector3 p2) => p1.Y != p2.Y ? (y - p1.Y) / (p2.Y - p1.Y) : 1;
        public static float Clamp(float value) => Clamp(value, 0, 1);
        public static float Clamp(float value, float min, float max) => Math.Max(min, Math.Min(max, value));
        public static float Interpolate(float min, float max, float position) => min + (max - min) * Clamp(position);
        public static bool WithinBounds(int x, int y, int xMin, int yMin, int xMax, int yMax) => x >= xMin && y >= yMin && x < xMax && y < yMax;
        public static float LineSide2D(Vector3 p, Vector3 lineFrom, Vector3 lineTo)
        {
            var p1 = Vector3.Subtract(p, lineFrom);
            var p2 = Vector3.Subtract(lineTo, lineFrom);
            return p1.X * p2.Y - p2.X * p1.Y;
        }
		public static (Vector3 p1, Vector3 p2, Vector3 p3) OrderVectorsByY(Vector3 p1, Vector3 p2, Vector3 p3)
		{
            Vector3 temp;
            if (p1.Y > p2.Y)
            {
                temp = p2;
                p2 = p1;
                p1 = temp;
            }

            if (p2.Y > p3.Y)
            {
                temp = p2;
                p2 = p3;
                p3 = temp;
            }

            if (p1.Y > p2.Y)
            {
                temp = p2;
                p2 = p1;
                p1 = temp;
            }
            return (p1, p2, p3);
        }
    }
}
