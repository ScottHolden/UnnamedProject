using System;
using System.Diagnostics;
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
            foreach ((Vector3 start, Vector3 end) in mesh.GetFaceLines())
                DrawLine(GetPoint(start, transformMatrix), GetPoint(end, transformMatrix));
        }
        private void DrawLine(Vector2 start, Vector2 end) => DrawLine((int)start.X, (int)start.Y, (int)end.X, (int)end.Y);
        private void DrawLine(int x0, int y0, int x1, int y1)
        {
            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = (x0 < x1) ? 1 : -1;
            int sy = (y0 < y1) ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                DrawPoint(x0, y0);

                if ((x0 == x1) && (y0 == y1))
                    break;
                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }
        }
        private Vector2 GetPoint(Vector3 vertex, Matrix4x4 transformMatrix)
        {
            var point = Vector3.Transform(vertex, transformMatrix);
            return new Vector2(point.X * _width + _width / 2.0f, -point.Y * _height + _height / 2.0f);
        }

        private void DrawPoint(int x, int y)
        {
            if (!WithinBounds(x, y, 0, 0, _width, _height))
                return;
            Set(x, y, 0, 0, 255, 255);
        }
        private static bool WithinBounds(int x, int y, int xMin, int yMin, int xMax, int yMax) => x >= xMin && y >= yMin && x < xMax && y < yMax;

        public byte[] GetBuffer() => _backBuffer;
        private void Clear(byte b, byte g, byte r, byte a)
        {
            for (int i = 0; i < _backBuffer.Length; i += BytesPerPixel)
                Set(i, b, g, r, a);
        }
        private void Set(int x, int y, byte b, byte g, byte r, byte a) =>
            Set((x + y * _width) * BytesPerPixel, b, g, r, a);

        private void Set(int i, byte b, byte g, byte r, byte a)
        {
            _backBuffer[i] = b;
            _backBuffer[i + 1] = g;
            _backBuffer[i + 2] = r;
            _backBuffer[i + 3] = a;
        }
    }
}
