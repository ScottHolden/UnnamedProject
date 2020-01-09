using System;

namespace UnnamedProject.Engine
{
    public struct Color : IEquatable<Color>
    {
        public byte R { get; }
        public byte G { get; }
        public byte B { get; }
        public byte A { get; }
        public Color(byte r, byte g, byte b, byte a)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

        // Let's be nice struct citizens 
        public override bool Equals(object? obj) => obj is Color color && Equals(color);
        public bool Equals(Color other) =>
            this.R == other.R &&
            this.G == other.G &&
            this.B == other.B &&
            this.A == other.A;
        public override int GetHashCode()
        {
            return HashCode.Combine(this.R, this.G, this.B, this.A);
        }

        public static bool operator ==(Color left, Color right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Color left, Color right)
        {
            return !(left == right);
        }
    }
}
