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
}
