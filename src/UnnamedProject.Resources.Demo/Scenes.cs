using System.Numerics;
using UnnamedProject.Engine;
using UnnamedProject.Engine.Misc;

namespace UnnamedProject.Resources.Demo
{
    public static class Scenes
    {
        public static Scene BoxScene()
        {
            var camera = new Camera(new Vector3(0, 0, 10.0f), Vector3.Zero);
            var scene = new Scene(camera);
            scene.AddMesh(Box());
            return scene;
        }

        private static Mesh Box() =>
        new SlowlyRotatingMesh(new[]{
                new Vector3(-3, 3, 3),
                new Vector3(3, 3, 3),
                new Vector3(-3, -3, 3),
                new Vector3(3, -3, 3),
                new Vector3(-3, 3, -3),
                new Vector3(3, 3, -3),
                new Vector3(3, -3, -3),
                new Vector3(-3, -3, -3)
            }, new[] {
                new Face(0, 1, 2),
				new Face(1, 2, 3),
				new Face(1, 3, 6),
				new Face(1, 5, 6),
				new Face(0, 1, 4),
				new Face(1, 4, 5),
				new Face(2, 3, 7),
				new Face(3, 6, 7),
				new Face(0, 2, 7),
				new Face(0, 4, 7),
				new Face(4, 5, 6),
				new Face(4, 6, 7),
            });
    }
}
