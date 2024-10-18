using System.Drawing;

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
