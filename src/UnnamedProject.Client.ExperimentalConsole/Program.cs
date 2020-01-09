using System;
using System.Drawing;

namespace UnnamedProject.Client.ExperimentalConsole
{
    public static class Program
    {
        public static void Main()
        {
            Size fontSize = NativeWrappers.GetConsoleFontSize();
            using var g = Graphics.FromHwnd(NativeWrappers.GetConsoleWindow());
            int width = (int)(Console.WindowWidth * fontSize.Width * g.DpiX / 96.0f);
            int height = (int)(Console.WindowHeight * fontSize.Height * g.DpiY / 96.0f);
            var game = new ConsoleGame(width, height);
            game.Run(g);
        }
    }
}
