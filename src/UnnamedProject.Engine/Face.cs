﻿using System.Numerics;

namespace UnnamedProject.Engine
{
    public struct Face
	{
        public int A { get; }
        public int B { get; }
        public int C { get; }
        public Color Color { get; }
        public Face(int a, int b, int c, Color color)
        {
            this.A = a;
            this.B = b;
            this.C = c;
            this.Color = color;
        }
    }
    public struct VectorFace
    {
        public Vector3 A { get; }
        public Vector3 B { get; }
        public Vector3 C { get; }
        public Color Color { get; }
        public VectorFace(Vector3 a, Vector3 b, Vector3 c, Color color)
        {
            this.A = a;
            this.B = b;
            this.C = c;
            this.Color = color;
        }
    }
}
