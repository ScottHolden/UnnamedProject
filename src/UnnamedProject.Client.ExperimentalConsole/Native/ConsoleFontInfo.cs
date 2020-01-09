using System.Runtime.InteropServices;

namespace UnnamedProject.Client.ExperimentalConsole
{
    [StructLayout(LayoutKind.Sequential)]
    public class ConsoleFontInfo
    {
        internal int nFont;
        internal Coord dwFontSize;
    }
}
