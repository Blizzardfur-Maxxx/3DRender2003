using System.Drawing;

namespace _DRender2003
{
    public class CubeRenderer : ShapeRenderer
    {
        private Entity cubeEntity;

        public CubeRenderer(Renderer renderer, Camera camera)
            : base(renderer, camera)
        {
            // Initialize cube entity at a specific position and no rotation
            cubeEntity = new Entity(new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1));
        }

        public override void DrawShape(Graphics g, Vector3 center, Vector3 size, Color[] colors, bool fillShapes)
        {
            // Update the scale of the cube entity based on the passed size
            cubeEntity.Rotate(new Vector3(0, 1, 0)); // Adjust this value for faster/slower rotation
            cubeEntity.SetScale(size);

            
            Vector3[] vertices = GetCubeVertices(cubeEntity.Scale);
            for (int i = 0; i < vertices.Length; i++)
            {
                // First apply the rotation, then translate to the cube's position
                vertices[i] = cubeEntity.ApplyRotation(vertices[i]) + cubeEntity.Position;
            }

            if (fillShapes)
            {
                DrawFilledCube(g, vertices, colors);
            }
            else
            {
                DrawCubeWireframe(g, vertices, colors[0]);
            }
        }

        private void DrawCubeWireframe(Graphics g, Vector3[] vertices, Color color)
        {
            // Apply perspective projection
            Vector3[] projectedVertices = ProjectVertices(vertices);

            // Draw the edges of the cube
            DrawLine(g, projectedVertices[0], projectedVertices[1], color); // Front bottom
            DrawLine(g, projectedVertices[1], projectedVertices[2], color); // Front right
            DrawLine(g, projectedVertices[2], projectedVertices[3], color); // Front top
            DrawLine(g, projectedVertices[3], projectedVertices[0], color); // Front left

            DrawLine(g, projectedVertices[4], projectedVertices[5], color); // Back bottom
            DrawLine(g, projectedVertices[5], projectedVertices[6], color); // Back right
            DrawLine(g, projectedVertices[6], projectedVertices[7], color); // Back top
            DrawLine(g, projectedVertices[7], projectedVertices[4], color); // Back left

            DrawLine(g, projectedVertices[0], projectedVertices[4], color); // Left edges
            DrawLine(g, projectedVertices[1], projectedVertices[5], color); // Right edges
            DrawLine(g, projectedVertices[2], projectedVertices[6], color); // Top edges
            DrawLine(g, projectedVertices[3], projectedVertices[7], color); // Bottom edges
        }

        private void DrawFilledCube(Graphics g, Vector3[] vertices, Color[] colors)
        {
            // Apply perspective projection
            Vector3[] projectedVertices = ProjectVertices(vertices);

            // Draw filled faces with specified colors
            DrawFace(g, projectedVertices[0], projectedVertices[1], projectedVertices[2], projectedVertices[3], colors[0]); // Front
            DrawFace(g, projectedVertices[4], projectedVertices[5], projectedVertices[6], projectedVertices[7], colors[1]); // Back
            DrawFace(g, projectedVertices[0], projectedVertices[3], projectedVertices[7], projectedVertices[4], colors[2]); // Left
            DrawFace(g, projectedVertices[1], projectedVertices[5], projectedVertices[6], projectedVertices[2], colors[3]); // Right
            DrawFace(g, projectedVertices[3], projectedVertices[2], projectedVertices[6], projectedVertices[7], colors[4]); // Top
            DrawFace(g, projectedVertices[0], projectedVertices[4], projectedVertices[5], projectedVertices[1], colors[5]); // Bottom
        }

        private Vector3[] GetCubeVertices(Vector3 size)
        {
            // Define the vertices centered around (0, 0, 0) with scaling
            return new Vector3[] {
                new Vector3(-size.X / 2, -size.Y / 2, -size.Z / 2), // Front bottom left
                new Vector3(size.X / 2, -size.Y / 2, -size.Z / 2), // Front bottom right
                new Vector3(size.X / 2, size.Y / 2, -size.Z / 2), // Front top right
                new Vector3(-size.X / 2, size.Y / 2, -size.Z / 2), // Front top left
                new Vector3(-size.X / 2, -size.Y / 2, size.Z / 2), // Back bottom left
                new Vector3(size.X / 2, -size.Y / 2, size.Z / 2), // Back bottom right
                new Vector3(size.X / 2, size.Y / 2, size.Z / 2), // Back top right
                new Vector3(-size.X / 2, size.Y / 2, size.Z / 2) // Back top left
            };
        }

        private void DrawFace(Graphics g, Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Color color)
        {
            // Use the depth buffer to check and update the depth before drawing each face
            float depth = (v1.Z + v2.Z + v3.Z + v4.Z) / 4f; // Average depth of the face
            DrawFaceWithDepth(g, v1, v2, v3, v4, color); // Use depth test
        }
    }
}
