using System.Numerics;

namespace UnnamedProject.Physics
{
    public class Entity
    {
        public Vector3 Position { get; }
        public Vector3 Velocity { get; }
        public Vector3 Acceleration { get; }

        public Entity(Vector3 position)
        {
            this.Position = position;
        }

        public virtual void Update(long deltaMS)
        {
        }
    }
}
