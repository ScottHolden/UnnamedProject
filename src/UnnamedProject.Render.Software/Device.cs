﻿using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnnamedProject.Engine;

namespace UnnamedProject.Render.Software
{
    // Todo: rename, as this is a BGRA Device
    // Todo: Add color profiles
    public class Device
    {
        private const int BytesPerPixel = 4; //BGRA
        // Todo: Refactor in a QuickClearBuffer
        private readonly byte[] _backBuffer;
        private readonly byte[] _defaultBackBuffer;
        private readonly float[] _depthBuffer;
        private readonly float[] _defaultDepthBuffer;
        private readonly Matrix4x4 _projectionMatrix;
        private readonly int _width;
        private readonly int _height;
        public Device(int width, int height)
        {
            _width = width;
            _height = height;
            _backBuffer = new byte[width * height * BytesPerPixel];
            _depthBuffer = new float[_backBuffer.Length];
            _projectionMatrix = Matrix4x4.CreatePerspective(0.78f, (float)height / width, 0.01f, 1.0f);

            // Quick clear setup
            _defaultBackBuffer = new byte[_backBuffer.Length];
            for (int i = 0; i < _backBuffer.Length; i += BytesPerPixel)
                _defaultBackBuffer[i + 3] = 255;

            _defaultDepthBuffer = new float[_depthBuffer.Length];
            for (int i = 0; i < _depthBuffer.Length; i++)
                _defaultDepthBuffer[i] = float.MaxValue;
        }

        public void Render(Scene scene)
        {
            Clear();
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
            foreach (VectorFace face in mesh.GetFaces())
                DrawTriangle(ProjectFace(face, transformMatrix));
        }

        private VectorFace ProjectFace(VectorFace face, Matrix4x4 transformMatrix) =>
            new VectorFace(
                Project(face.A, transformMatrix),
                Project(face.B, transformMatrix),
                Project(face.C, transformMatrix),
                face.Color);

        private void DrawTriangle(VectorFace face) => DrawTriangle(face.A, face.B, face.C, face.Color);

        private void DrawTriangle(Vector3 p1, Vector3 p2, Vector3 p3, Color color)
		{
            (p1, p2, p3) = MathHelper.OrderVectorsByY(p1, p2, p3);

            bool left = MathHelper.LineSide2D(p2, p1, p3) > 0;

            for (int y = (int)p1.Y; y <= (int)p3.Y; y++)
            {
                if (left)
                {
                    if (y < p2.Y)
                    {
                        DrawScanLine(y, p1, p3, p1, p2, color);
                    }
                    else
                    {
                        DrawScanLine(y, p1, p3, p2, p3, color);
                    }
                }
				else
				{
                    if (y < p2.Y)
                    {
                        DrawScanLine(y, p1, p2, p1, p3, color);
                    }
                    else
                    {
                        DrawScanLine(y, p2, p3, p1, p3, color);
                    }
                }
            }
        }

        private void DrawScanLine(int y, Vector3 pa, Vector3 pb, Vector3 pc, Vector3 pd, Color color)
        {
            (float z1, int sx) = MathHelper.InterpolateZX(y, pa, pb);
            (float z2, int ex) = MathHelper.InterpolateZX(y, pc, pd);

            for (int x = sx; x < ex; x++)
            {
                float gradient = (x - sx) / (float)(ex - sx);
                float z = MathHelper.Interpolate(z1, z2, gradient);
                DrawPoint(x, y, z, color);
            }
        }

        private Vector3 Project(Vector3 vertex, Matrix4x4 transformMatrix)
        {
            var point = Vector3.Transform(vertex, transformMatrix);
            return new Vector3(point.X * _width + _width / 2.0f, -point.Y * _height + _height / 2.0f, point.Z);
        }

        private void DrawPoint(int x, int y, float z, Color color)
        {
            if (!MathHelper.WithinBounds(x, y, 0, 0, _width, _height))
                return;
            int i = XYToIndex(x, y);
            if (_depthBuffer[i] < z)
                return;
            _depthBuffer[i] = z;
            Set(i, color.B, color.G, color.R, color.A);
        }

        public byte[] GetBuffer() => _backBuffer;
        private void Clear()
        {
            Array.Copy(_defaultBackBuffer, 0, _backBuffer, 0, _backBuffer.Length);
            Array.Copy(_defaultDepthBuffer, 0, _depthBuffer, 0, _depthBuffer.Length);
        }
        private int XYToIndex(int x, int y) => (x + y * _width) * BytesPerPixel;

        // Let's get aggressive!
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Set(int i, byte b, byte g, byte r, byte a)
        {
            _backBuffer[i] = b;
            _backBuffer[i + 1] = g;
            _backBuffer[i + 2] = r;
            _backBuffer[i + 3] = a;
        }
    }
}
