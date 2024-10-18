using System.Drawing;

namespace _DRender2003
{
    public abstract class ShapeRenderer
    {
        protected Renderer renderer;
        protected Camera camera;
        protected float[,] depthBuffer;

        public ShapeRenderer(Renderer renderer, Camera camera)
        {
            this.renderer = renderer;
            this.camera = camera;

            // Initialize depth buffer
            depthBuffer = new float[Renderer.SCREEN_WIDTH, Renderer.SCREEN_HEIGHT];
        }

        // Method to clear the depth buffer before rendering each frame
        public void ClearDepthBuffer()
        {
            for (int x = 0; x < Renderer.SCREEN_WIDTH; x++)
            {
                for (int y = 0; y < Renderer.SCREEN_HEIGHT; y++)
                {
                    depthBuffer[x, y] = float.MaxValue;
                }
            }
        }

        
        public abstract void DrawShape(Graphics g, Vector3 center, float size, Color[] colors, bool fillShapes);

        // Method for drawing a pixel with depth checking
        protected void DrawPixelWithDepth(Graphics g, int x, int y, float depth, Color color)
        {
            if (x >= 0 && x < Renderer.SCREEN_WIDTH && y >= 0 && y < Renderer.SCREEN_HEIGHT)
            {
                
                if (depth < depthBuffer[x, y])
                {
                    depthBuffer[x, y] = depth;
                    g.FillRectangle(new SolidBrush(color), x, y, 1, 1);
                }
            }
        }

        // Method for projecting 3D vertices to 2D screen coordinates using the camera
        protected Vector3[] ProjectVertices(Vector3[] vertices)
        {
            Vector3[] projectedVertices = new Vector3[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {

                projectedVertices[i] = camera.Project(vertices[i]);
            }
            return projectedVertices;
        }

        // Method for drawing lines between 2D projected points
        protected void DrawLine(Graphics g, Vector3 p1, Vector3 p2, Color color)
        {
            int x1 = (int)p1.X;
            int y1 = (int)p1.Y;
            int x2 = (int)p2.X;
            int y2 = (int)p2.Y;


            renderer.lineRenderer.DrawLine(g, x1, y1, x2, y2, color);
        }


        protected void DrawFaceWithDepth(Graphics g, Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Color color)
        {
            Point[] points = new Point[] {
                new Point((int)v1.X, (int)v1.Y),
                new Point((int)v2.X, (int)v2.Y),
                new Point((int)v3.X, (int)v3.Y),
                new Point((int)v4.X, (int)v4.Y),
            };

            using (Brush brush = new SolidBrush(color))
            {
                g.FillPolygon(brush, points);
            }
        }
    }
}
