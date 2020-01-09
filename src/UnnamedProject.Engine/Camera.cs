using System.Numerics;

namespace UnnamedProject.Engine
{
    public class Camera
    {
		public Vector3 Position { get; }
        public Vector3 Target { get; }
        public Camera(Vector3 position, Vector3 target)
        {
            this.Position = position;
            this.Target = target;
        }
    }
}
