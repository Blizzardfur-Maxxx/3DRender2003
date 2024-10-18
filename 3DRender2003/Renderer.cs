using System.Drawing;
using System.Windows.Forms;
using System; // Make sure to include this for Keys enum

namespace _DRender2003
{
    public class Renderer
    {
        public const int SCREEN_WIDTH = 240;
        public const int SCREEN_HEIGHT = 320;
        public Bitmap framebuffer;
        public LineRenderer lineRenderer;
        private CubeRenderer cubeRenderer;
        public bool fillShapes = true;

        private Camera camera;

        public Renderer()
        {
            // Initialize the frame buffer and shape renderers
            framebuffer = new Bitmap(SCREEN_WIDTH, SCREEN_HEIGHT);
            lineRenderer = new LineRenderer(this);
            camera = new Camera(new Vector3(0, 0, -5), new Vector3(0, 0, 0), 1.0f);
            cubeRenderer = new CubeRenderer(this, camera);
        }

        // Public property to access the camera
        public Camera Camera
        {
            get { return camera; }
        }

        // Method to move the camera based on user input
        public void MoveCamera(Keys key)
        {
            const float moveSpeed = 20f; // Adjust the speed as necessary
            switch (key)
            {
                case Keys.W: // Move forward (in the Z direction)
                    camera.Position += new Vector3(0, 0, moveSpeed); // Move forward in the Z direction
                    break;
                case Keys.S: // Move backward (in the Z direction)
                    camera.Position += new Vector3(0, 0, -moveSpeed); // Move backward in the Z direction
                    break;
                case Keys.A: // Move left
                    camera.Position += new Vector3(-moveSpeed, 0, 0); // Move left in the X direction
                    break;
                case Keys.D: // Move right
                    camera.Position += new Vector3(moveSpeed, 0, 0); // Move right in the X direction
                    break;
            }
            Console.WriteLine("Camera Position: " + camera.Position.X + ", " + camera.Position.Y + ", " + camera.Position.Z);
        }

        // Method to render the frame
        public void RenderFrame()
        {
            using (Graphics g = Graphics.FromImage(framebuffer))
            {
                g.Clear(Color.Black);

                cubeRenderer.ClearDepthBuffer();

                // Call the method to render the cube
                RenderCube(g);
            }
        }

        // Method to render the cube
        public void RenderCube(Graphics g)
        {
            Vector3 cubeCenter = new Vector3(120, 160, 0);
            Color[] cubeColors = new Color[] 
            {
                Color.Red,   // Front face
                Color.Green, // Back face
                Color.Blue,  // Left face
                Color.Yellow,// Right face
                Color.Cyan,  // Top face
                Color.Magenta // Bottom face
            };

            // Pass an array of colors to DrawCube
            cubeRenderer.DrawShape(g, cubeCenter, 100, cubeColors, fillShapes);
        }
    }
}
