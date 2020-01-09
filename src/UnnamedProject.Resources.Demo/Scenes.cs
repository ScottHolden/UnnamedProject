using System.Numerics;
using UnnamedProject.Engine;
using UnnamedProject.Engine.Misc;

namespace UnnamedProject.Resources.Demo
{
    public static class Scenes
	{
		public static Scene BoxScene()
		{
            var scene = new Scene();
            var boxMesh = new SlowlyRotatingMesh(new[]{
                new Vector3(-1, 1, 1),
                new Vector3(1, 1, 1),
                new Vector3(-1, -1, 1),
                new Vector3(-1, -1, -1),
                new Vector3(-1, 1, -1),
                new Vector3(1, 1, -1),
                new Vector3(1, -1, 1),
                new Vector3(1, -1, -1)
            });
            scene.AddMesh(boxMesh);
            return scene;
        }
	}
}
