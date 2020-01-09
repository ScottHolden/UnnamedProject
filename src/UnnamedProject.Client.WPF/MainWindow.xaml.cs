using System;
using System.Numerics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using UnnamedProject.Engine;
using UnnamedProject.Render.Software;

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
        private readonly Mesh _boxMesh;
        public MainWindow()
		{
            InitializeComponent();

            _width = (int)displayImage.Width;
            _height = (int)displayImage.Height;
            _fullRect = new Int32Rect(0, 0, _width, _height);
            _stride = _width * _pixelFormat.BitsPerPixel / 8;

            _device = new Device(_width, _height);
            _scene = new Scene();
            _boxMesh = new Mesh(new[]{
                new Vector3(-1, 1, 1),
                new Vector3(1, 1, 1),
                new Vector3(-1, -1, 1),
                new Vector3(-1, -1, -1),
                new Vector3(-1, 1, -1),
                new Vector3(1, 1, -1),
                new Vector3(1, -1, 1),
                new Vector3(1, -1, -1)
            });
            _scene.AddMesh(_boxMesh);
            _frontBuffer = new WriteableBitmap(_width, _height, DPI, DPI, _pixelFormat, null);
            displayImage.Source = _frontBuffer;

            CompositionTarget.Rendering += CompositionTarget_Rendering;
		}

        private void CompositionTarget_Rendering(object? sender, EventArgs e)
        {
            _boxMesh.Rotate(new Vector3(0.01f, 0.01f, 0));
            if (_device != null)
            {
                _device.Render(_scene);
                _frontBuffer.WritePixels(_fullRect, _device.GetBuffer(), _stride, 0);
            }
        }
    }
}
