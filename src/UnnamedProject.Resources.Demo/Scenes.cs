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
        private static readonly Color Red = new Color(255, 0, 0, 255);
        private static readonly Color Green = new Color(0, 255, 0, 255);
        private static readonly Color Blue = new Color(0, 0, 255, 255);
        private static readonly Color White = new Color(255, 255, 255, 255);
        private static readonly Color Orange = new Color(255, 85, 30, 255);
        private static readonly Color Yellow = new Color(255, 225, 50, 255);
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
            }, new Face[] {
                new Face(0, 1, 2, Red  ),
                new Face(1, 2, 3, Red),
                new Face(1, 3, 6, Blue ),
                new Face(1, 5, 6, Blue),
                new Face(0, 1, 4, Green),
                new Face(1, 4, 5, Green  ),
                new Face(2, 3, 7, Orange   ),
                new Face(3, 6, 7, Orange ),
                new Face(0, 2, 7, Yellow  ),
                new Face(0, 4, 7, Yellow   ),
                new Face(4, 5, 6, White ),
                new Face(4, 6, 7, White  )
            });
    }
}
