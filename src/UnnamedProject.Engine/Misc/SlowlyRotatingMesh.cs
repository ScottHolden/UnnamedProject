using System.Numerics;

namespace UnnamedProject.Engine.Misc
{
    public class SlowlyRotatingMesh : Mesh
    {
        public SlowlyRotatingMesh(Vector3[] vertices, Face[] faces) : base(vertices, faces)
        {
        }
        public override void Update(long deltaMS)
        {
            float distance = deltaMS / 1000.0f;
            Rotate(new Vector3(distance, distance/2, 0));
        }
    }
}
