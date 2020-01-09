using System.Runtime.InteropServices;

namespace UnnamedProject.Client.ExperimentalConsole
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Coord
    {
        [FieldOffset(0)]
        internal short X;
        [FieldOffset(2)]
        internal short Y;
    }
}
