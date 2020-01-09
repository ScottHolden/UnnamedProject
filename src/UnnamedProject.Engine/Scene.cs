using System.Collections.Generic;

namespace UnnamedProject.Engine
{
    public class Scene
    {
        public Scene(Camera mainCamera)
        {
            this.MainCamera = mainCamera;
        }

        public Camera MainCamera { get; }
        public List<Mesh> Meshes { get; } = new List<Mesh>();

        public void AddMesh(Mesh mesh) => this.Meshes.Add(mesh);

		public void Update(long deltaMS)
		{
            foreach (Mesh m in this.Meshes)
                m.Update(deltaMS);
        }
    }
}
