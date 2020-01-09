using System.Numerics;
using UnnamedProject.Engine;
using UnnamedProject.Engine.Misc;

namespace UnnamedProject.Resources.Demo
{
    public static class Scenes
    {
        public static Scene BoxScene()
        {
            var camera = new Camera(new Vector3(10.0f, 10.0f, 10.0f), Vector3.Zero);
            var scene = new Scene(camera);
            scene.AddMesh(Box());
            scene.AddMesh(Plane(6));
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
            }, new Face[] {
                new Face(0, 1, 2, Colors.Red  ),
                new Face(1, 2, 3, Colors.Red),
                new Face(1, 3, 6, Colors.Blue ),
                new Face(1, 5, 6, Colors.Blue),
                new Face(0, 1, 4, Colors.Green),
                new Face(1, 4, 5, Colors.Green  ),
                new Face(2, 3, 7, Colors.Orange   ),
                new Face(3, 6, 7, Colors.Orange ),
                new Face(0, 2, 7, Colors.Yellow  ),
                new Face(0, 4, 7, Colors.Yellow   ),
                new Face(4, 5, 6, Colors.White ),
                new Face(4, 6, 7, Colors.White  )
            });
        private static Mesh Plane(int size) =>
        new Mesh(new Vector3(0, -size, 0),
            new[]{
                new Vector3(-size,0,-size),
                new Vector3(-size,0,size),
                new Vector3(size,0,size),
                new Vector3(size,0,-size),
            }, new Face[] {
                new Face(0,1,2, Colors.LightBlue),
                new Face(0,3,2, Colors.LightBlue),
            });
    }
}
