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
        private SphereRenderer sphereRenderer;
        private PyramidRenderer pyramidRenderer;
        public bool fillShapes = true;
        public enum RendererType { Cube, Sphere, Pyramid }
        private RendererType currentRendererType = RendererType.Cube;

        private Camera camera;

        public Renderer()
        {
            // Initialize the frame buffer and shape renderers
            framebuffer = new Bitmap(SCREEN_WIDTH, SCREEN_HEIGHT);
            lineRenderer = new LineRenderer(this);
            camera = new Camera(new Vector3(0, 0, -5), new Vector3(0, 0, 0), 1.0f);
            cubeRenderer = new CubeRenderer(this, camera);
            pyramidRenderer = new PyramidRenderer(this, camera);
            sphereRenderer = new SphereRenderer(this, camera, 8, 8);

        }

        public void SetRendererType(RendererType type)
        {
            currentRendererType = type;
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
                case Keys.Up: // Move forward (in the Z direction)
                    camera.Position += new Vector3(0, 0, -moveSpeed); // Move forward in the Z direction
                    break;
                case Keys.Down: // Move backward (in the Z direction)
                    camera.Position += new Vector3(0, 0, moveSpeed); // Move backward in the Z direction
                    break;
                case Keys.Left: // Move left
                    camera.Position += new Vector3(moveSpeed, 0, 0); // Move left in the X direction
                    break;
                case Keys.Right: // Move right
                    camera.Position += new Vector3(-moveSpeed, 0, 0); // Move right in the X direction
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

                cubeRenderer.ClearBackDepthBuffer();

                // Render based on the current renderer type
                switch (currentRendererType)
                {
                    case RendererType.Cube:
                        RenderCube(g);
                        break;
                    case RendererType.Sphere:
                        RenderSphere(g);
                        break;
                    case RendererType.Pyramid:
                        RenderPyramid(g);
                        break;
                }
            }
        }

        // Method to render the cube
        public void RenderCube(Graphics g)
        {
            Vector3 cubeCenter = new Vector3(0, 0, 0);
            Color[] cubeColors = new Color[] 
            {
                Color.Red,   // Front face
                Color.Green, // Back face
                Color.Blue,  // Left face
                Color.Yellow,// Right face
                Color.Cyan,  // Top face
                Color.Magenta // Bottom face
            };

            float cubeSizeValue = 100f; 
            Vector3 cubeSize = new Vector3(cubeSizeValue, cubeSizeValue, cubeSizeValue); 
            cubeRenderer.DrawShape(g, cubeCenter, cubeSize, cubeColors, fillShapes);
        }

        public void RenderPyramid(Graphics g)
        {
            Vector3 pyramidCenter = new Vector3(0, 0, 0);
            Color[] pyramidColors = new Color[] 
            {
                Color.Red,   // Front face
                Color.Green, // Back face
                Color.Blue,  // Left face
                Color.Yellow,// Right face
                Color.Cyan,  // Top face
                Color.Magenta // Bottom face
            };

            float pyramidSizeValue = 100f; 
            Vector3 pyramidSize = new Vector3(pyramidSizeValue, pyramidSizeValue, pyramidSizeValue); 
            pyramidRenderer.DrawShape(g, pyramidCenter, pyramidSize, pyramidColors, fillShapes);
        }

        public void RenderSphere(Graphics g)
        {
            Vector3 sphereCenter = new Vector3(0, 0, 0); // Define center of the sphere
            Color[] sphereColors = new Color[] 
            {
                Color.Red,   // Front face
                Color.Green, // Back face
                Color.Blue,  // Left face
                Color.Yellow,// Right face
                Color.Cyan,  // Top face
                Color.Magenta // Bottom face
            };

            float sphereDiameterValue = 100f; 
            Vector3 sphereDiameter = new Vector3(sphereDiameterValue, sphereDiameterValue, sphereDiameterValue); 
            sphereRenderer.DrawShape(g, sphereCenter, sphereDiameter, sphereColors, fillShapes);
        }
    }
}
