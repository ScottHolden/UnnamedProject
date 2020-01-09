using System.Collections.Generic;
using System.Numerics;
using UnnamedProject.Physics;

namespace UnnamedProject.Engine
{
    public class Mesh : Entity
    {
        private readonly Vector3[] _vertices;
        public Vector3 Rotation { get; private set; }

        public Mesh(Vector3[] vertices) : this(Vector3.Zero, vertices)
        {
        }

        public Mesh(Vector3 position, Vector3[] vertices) : base(position)
        {
            _vertices = vertices;
        }

        public IEnumerable<Vector3> GetVerticies()
        {
            for (int i = 0; i < _vertices.Length; i++)
                yield return _vertices[i];
        }

        public void Rotate(Vector3 delta)
        {
            this.Rotation = Vector3.Add(this.Rotation, delta);
        }

        public Matrix4x4 GetWorldMatrix() => Matrix4x4.CreateFromYawPitchRoll(this.Rotation.Y, this.Rotation.X, this.Rotation.Z) *
                                                Matrix4x4.CreateTranslation(this.Position);
    }
}
