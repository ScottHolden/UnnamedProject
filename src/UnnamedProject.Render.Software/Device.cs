using System.Numerics;
using UnnamedProject.Engine;

namespace UnnamedProject.Render.Software
{
    // Todo: rename, as this is a BGRA Device
    // Todo: Add color profiles
    public class Device
	{
        private const int BytesPerPixel = 4; //BGRA
        private readonly byte[] _backBuffer;
        private readonly Matrix4x4 _projectionMatrix;
        private readonly int _width;
        private readonly int _height;
        public Device(int width, int height)
        {
            _width = width;
            _height = height;
            _backBuffer = new byte[width * height * BytesPerPixel];
            _projectionMatrix = Matrix4x4.CreatePerspective(0.78f, (float)height / width, 0.01f, 1.0f);
        }

        public void Render(Scene scene)
        {
            Clear(0, 0, 0, 255);
            DrawScene(scene);
        }

		private void DrawScene(Scene scene)
		{
            var viewMatrix = Matrix4x4.CreateLookAt(scene.MainCamera.Position, scene.MainCamera.Target, Vector3.UnitY);
            foreach (Mesh mesh in scene.Meshes)
                DrawMesh(mesh, viewMatrix);
        }
		private void DrawMesh(Mesh mesh, Matrix4x4 viewMatrix)
		{
            Matrix4x4 worldMatrix = mesh.GetWorldMatrix();
            Matrix4x4 transformMatrix = worldMatrix * viewMatrix * _projectionMatrix;
            foreach (Vector3 vertex in mesh.GetVerticies())
                DrawVertex(vertex, transformMatrix);
        }
		private void DrawVertex(Vector3 vertex, Matrix4x4 transformMatrix)
		{
            var point = Vector3.Transform(vertex, transformMatrix);
            int x = (int)(point.X * _width + _width / 2.0f);
            int y = (int)(-point.Y * _height + _height / 2.0f);
            if (x >= 0 && y >= 0 &&
                x < _width && y < _height)
            {
                Set(x, y, 0, 0, 255, 255);
            }
        }

        public byte[] GetBuffer() => _backBuffer;
		private void Clear(byte b, byte g, byte r, byte a)
		{
            for (int i = 0; i < _backBuffer.Length; i += BytesPerPixel)
                Set(i, b, g, r, a);
        }
        private void Set(int x, int y, byte b, byte g, byte r, byte a) =>
            Set((x + y * _width)* BytesPerPixel, b, g, r, a);

        private void Set(int i, byte b, byte g, byte r, byte a)
		{
            _backBuffer[i] = b;
            _backBuffer[i + 1] = g;
            _backBuffer[i + 2] = r;
            _backBuffer[i + 3] = a;
        }
    }
}
