using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using UnnamedProject.Engine;
using UnnamedProject.Render.Software;
using UnnamedProject.Resources.Demo;

namespace UnnamedProject.Client.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Device? _device;
        private readonly Scene _scene;
        private readonly WriteableBitmap _frontBuffer;
        private readonly int _width;
        private readonly int _height;
        private readonly int _stride;
        private readonly Int32Rect _fullRect;
        private const double DPI = 96.0;
        private readonly PixelFormat _pixelFormat = PixelFormats.Bgra32;
        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();
        private long _lastFrame = 0;
        private long _lastUpdate = 0;
        private const int SmallestUpdateStep = 5;
        private const int TargetFPS = 100;
        private const int MsBetweenFrames = 1000 / TargetFPS;
        public MainWindow()
        {
            InitializeComponent();

            _width = (int)displayImage.Width;
            _height = (int)displayImage.Height;
            _fullRect = new Int32Rect(0, 0, _width, _height);
            _stride = _width * _pixelFormat.BitsPerPixel / 8;
            _device = new Device(_width, _height);
            _scene = Scenes.BoxScene();
            _frontBuffer = new WriteableBitmap(_width, _height, DPI, DPI, _pixelFormat, null);
            displayImage.Source = _frontBuffer;

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object? sender, EventArgs e)
        {
            long current = _stopwatch.ElapsedMilliseconds;
            long sinceLastFrame = current - _lastFrame;
            if (_device != null && sinceLastFrame > MsBetweenFrames)
            {
                _device.Render(_scene);
                _frontBuffer.WritePixels(_fullRect, _device.GetBuffer(), _stride, 0);
                _lastFrame = current;
                this.Title = (1000.0f / sinceLastFrame) + "FPS";
            }
            while (current - _lastUpdate > SmallestUpdateStep)
            {
                _scene.Update(SmallestUpdateStep);
                _lastUpdate += SmallestUpdateStep;
            }
        }
    }
}
