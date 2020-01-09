namespace UnnamedProject.Engine
{
    public struct Color
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
    }
}
