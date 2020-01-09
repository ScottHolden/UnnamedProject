using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using UnnamedProject.Engine;
using UnnamedProject.Render.Software;
using UnnamedProject.Resources.Demo;

namespace UnnamedProject.Client.ExperimentalConsole
{
    public class ConsoleGame
    {
        private readonly Device _device;
        private readonly Scene _scene;
        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();
        private long _lastFrame = 0;
        private long _lastUpdate = 0;
        private const int SmallestUpdateStep = 5;
        private const int TargetFPS = 60;
        private const int MsBetweenFrames = 1000 / TargetFPS;
        private double _avgFPS = TargetFPS;
        private const int AvgFPSFilter = 10;
        private int _fpsUpdate = 0;
        private readonly int _width;
        private readonly int _height;
        private readonly PixelFormat _pixelFormat = PixelFormat.Format32bppArgb;
        private const int BitsPerPixel = 32;
        public ConsoleGame(int width, int height)
        {
            _width = width;
            _height = height;
            _device = new Device(width, height);
            _scene = Scenes.BoxScene();
        }

        public void Run(Graphics g)
        {
            unsafe {
                fixed (byte* buffer = &_device.GetBuffer()[0])
                {
                    var stride = _width * BitsPerPixel / 8;
                    var b = new Bitmap(_width, _height, stride, _pixelFormat, (IntPtr) buffer);
                    var imageDrawRectangle = new Rectangle(0, 0, _width, _height);
                    while (true)
                    {
                        long current = _stopwatch.ElapsedMilliseconds;
                        long sinceLastFrame = current - _lastFrame;
                        if (sinceLastFrame > MsBetweenFrames)
                        {
                            _device.Render(_scene);
                            g.DrawImage(b, imageDrawRectangle);
                            _lastFrame = current;
                            _avgFPS = (_avgFPS * (AvgFPSFilter - 1) + (1000.0 / sinceLastFrame)) / AvgFPSFilter;
                            if (_fpsUpdate++ > AvgFPSFilter / 2)
                            {
                                string fps = Math.Round(_avgFPS, 2) + " FPS";
                                Debug.WriteLine(fps);
                                _fpsUpdate = 0;
                            }
                        }
                        while (current - _lastUpdate > SmallestUpdateStep)
                        {
                            _scene.Update(SmallestUpdateStep);
                            _lastUpdate += SmallestUpdateStep;
                        }
                        Thread.Sleep(0);
                    }
                }
            }
        }
    }
}
