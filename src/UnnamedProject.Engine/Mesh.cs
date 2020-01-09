using System.Numerics;
using UnnamedProject.Physics;

namespace UnnamedProject.Engine
{
    public class Mesh : Entity
    {
        private readonly Vector3[] _vertices;
        private readonly VectorFace[] _faces;

        public Vector3 Rotation { get; private set; }

        public Mesh(Vector3[] vertices, Face[] faces) : this(Vector3.Zero, vertices, faces)
        {
        }
        public Mesh(Vector3 position, Vector3[] vertices, Face[] faces) : base(position)
        {
            _vertices = vertices;
            _faces = new VectorFace[faces.Length];
			for(int i=0;i<_faces.Length;i++)
			{
                _faces[i] = new VectorFace(_vertices[faces[i].A], _vertices[faces[i].B], _vertices[faces[i].C], faces[i].Color);
            }
        }
        public VectorFace[] GetFaces() => _faces;
        public void Rotate(Vector3 delta)
        {
            this.Rotation = Vector3.Add(this.Rotation, delta);
        }

        public Matrix4x4 GetWorldMatrix() => Matrix4x4.CreateFromYawPitchRoll(this.Rotation.Y, this.Rotation.X, this.Rotation.Z) *
                                                Matrix4x4.CreateTranslation(this.Position);
    }
}
