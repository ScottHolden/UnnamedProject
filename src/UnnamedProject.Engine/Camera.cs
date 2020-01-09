using System.Numerics;

namespace UnnamedProject.Engine
{
    public class Camera
    {
        public Vector3 Position { get; } = new Vector3(0, 0, 10.0f);
        public Vector3 Target { get; } = Vector3.Zero;
    }
}
