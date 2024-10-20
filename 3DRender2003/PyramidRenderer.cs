using System.Drawing;

namespace _DRender2003
{
    public class PyramidRenderer : ShapeRenderer
    {
        private Entity pyramidEntity;

        public PyramidRenderer(Renderer renderer, Camera camera)
            : base(renderer, camera)
        {
            // Initialize pyramid entity at a specific position and no rotation
            pyramidEntity = new Entity(new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1));
        }

        public override void DrawShape(Graphics g, Vector3 center, Vector3 size, Color[] colors, bool fillShapes)
        {
            // Set the pyramid size directly
            pyramidEntity.SetScale(size); // Set the size for the pyramid

            // Rotate the pyramid by a small amount (e.g., 1 degree) around the Y axis
            pyramidEntity.Rotate(new Vector3(0, 1, 0)); // Adjust this value for faster/slower rotation

            // Apply rotation to vertices, centered around the entity's position
            Vector3[] vertices = GetPyramidVertices(pyramidEntity.Scale.X);
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = pyramidEntity.ApplyRotation(vertices[i]) + pyramidEntity.Position;
            }

            if (fillShapes)
            {
                DrawFilledPyramid(g, vertices, colors);
            }
            else
            {
                DrawPyramidWireframe(g, vertices, Color.Red);
            }
        }

        private void DrawPyramidWireframe(Graphics g, Vector3[] vertices, Color color)
        {
            // Apply perspective projection
            Vector3[] projectedVertices = ProjectVertices(vertices);

            // Draw the edges of the pyramid
            DrawLine(g, projectedVertices[0], projectedVertices[1], color);
            DrawLine(g, projectedVertices[1], projectedVertices[2], color);
            DrawLine(g, projectedVertices[2], projectedVertices[3], color);
            DrawLine(g, projectedVertices[3], projectedVertices[0], color);

            // Draw the sides of the pyramid
            DrawLine(g, projectedVertices[0], projectedVertices[4], color);
            DrawLine(g, projectedVertices[1], projectedVertices[4], color);
            DrawLine(g, projectedVertices[2], projectedVertices[4], color);
            DrawLine(g, projectedVertices[3], projectedVertices[4], color);
        }

        private void DrawFilledPyramid(Graphics g, Vector3[] vertices, Color[] colors)
        {
            // Apply perspective projection
            Vector3[] projectedVertices = ProjectVertices(vertices);

            // Draw filled faces with specified colors
            DrawFaceWithDepth(g, projectedVertices[0], projectedVertices[1], projectedVertices[2], projectedVertices[4], colors[0]); // Front face
            DrawFaceWithDepth(g, projectedVertices[1], projectedVertices[2], projectedVertices[3], projectedVertices[4], colors[1]); // Right face
            DrawFaceWithDepth(g, projectedVertices[2], projectedVertices[3], projectedVertices[0], projectedVertices[4], colors[2]); // Back face
            DrawFaceWithDepth(g, projectedVertices[3], projectedVertices[0], projectedVertices[1], projectedVertices[4], colors[3]); // Left face

            // Base face if you want to include it
            DrawFaceWithDepth(g, projectedVertices[0], projectedVertices[1], projectedVertices[2], projectedVertices[3], colors[4]); // Base face
        }

        private Vector3[] GetPyramidVertices(float size)
        {
            // Define the vertices centered around (0, 0, 0)
            return new Vector3[] {
                new Vector3(-size / 2, 0, -size / 2), // Base front left
                new Vector3(size / 2, 0, -size / 2), // Base front right
                new Vector3(size / 2, 0, size / 2), // Base back right
                new Vector3(-size / 2, 0, size / 2), // Base back left
                new Vector3(0, -size, 0) // Apex of the pyramid
            };
        }
    }
}
