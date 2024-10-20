using System.Drawing;
using System.Collections.Generic;

namespace _DRender2003
{
    public class SphereRenderer : ShapeRenderer
    {
        private Entity sphereEntity;
        private int latitudeSegments;
        private int longitudeSegments;
        private float[,] depthBuffer;

        public SphereRenderer(Renderer renderer, Camera camera, int latSegments, int lonSegments)
            : base(renderer, camera)
        {
            // Initialize the sphere entity at a specific position and no rotation
            sphereEntity = new Entity(new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1));
            depthBuffer = new float[Renderer.SCREEN_WIDTH, Renderer.SCREEN_HEIGHT];
            latitudeSegments = latSegments;
            longitudeSegments = lonSegments;
        }

        public override void DrawShape(Graphics g, Vector3 center, Vector3 radius, Color[] colors, bool fillShapes)
        {
            // Clear the depth buffer before rendering each shape
            ClearDepthBuffer();

            // Rotate the sphere slightly around the Y axis
            sphereEntity.Rotate(new Vector3(0, 1, 0)); // Adjust for faster/slower rotation
            sphereEntity.SetScale(radius); // Apply scaling

            // Generate vertices for the sphere
            List<Vector3> vertices = GetSphereVertices(radius); // Directly use the radius

            // Apply rotation and translation to the sphere's position
            for (int i = 0; i < vertices.Count; i++)
            {
                vertices[i] = sphereEntity.ApplyRotation(vertices[i]) + sphereEntity.Position;
            }

            if (fillShapes)
            {
                DrawFilledSphere(g, vertices, colors);
            }
            else
            {
                DrawSphereWireframe(g, vertices, Color.Red);
            }
        }

        private List<Vector3> GetSphereVertices(Vector3 radius)
        {
            List<Vector3> vertices = new List<Vector3>();

            for (int lat = 0; lat <= latitudeSegments; lat++)
            {
                float theta = lat * (float)MathHelper.PI / latitudeSegments; // Latitude angle
                float sinTheta = (float)MathHelper.Sin(theta);
                float cosTheta = (float)MathHelper.Cos(theta);

                for (int lon = 0; lon <= longitudeSegments; lon++)
                {
                    float phi = lon * 2 * (float)MathHelper.PI / longitudeSegments; // Longitude angle
                    float sinPhi = (float)MathHelper.Sin(phi);
                    float cosPhi = (float)MathHelper.Cos(phi);

                    float x = radius.X * cosPhi * sinTheta; // Use radius.X for uniform scaling
                    float y = radius.Y * cosTheta;           // Use radius.Y for uniform scaling
                    float z = radius.Z * sinPhi * sinTheta; // Use radius.Z for uniform scaling

                    vertices.Add(new Vector3(x, y, z));
                }
            }

            return vertices;
        }

        private void DrawSphereWireframe(Graphics g, List<Vector3> vertices, Color color)
        {
            Vector3[] projectedVertices = ProjectVertices(vertices.ToArray());

            for (int lat = 0; lat < latitudeSegments; lat++)
            {
                for (int lon = 0; lon < longitudeSegments; lon++)
                {
                    int current = lat * (longitudeSegments + 1) + lon;
                    int next = current + 1;
                    int below = current + (longitudeSegments + 1);

                    DrawLine(g, projectedVertices[current], projectedVertices[next], color);

                    if (lat < latitudeSegments - 1)
                    {
                        DrawLine(g, projectedVertices[current], projectedVertices[below], color);
                    }
                }
            }
        }

        private void DrawFilledSphere(Graphics g, List<Vector3> vertices, Color[] colors)
        {
            Vector3[] projectedVertices = ProjectVertices(vertices.ToArray());

            for (int lat = 0; lat < latitudeSegments; lat++)
            {
                for (int lon = 0; lon < longitudeSegments; lon++)
                {
                    int current = lat * (longitudeSegments + 1) + lon;
                    int next = current + 1;
                    int below = current + (longitudeSegments + 1);
                    int belowNext = below + 1;

                    DrawTriangle(g, projectedVertices[current], projectedVertices[next], projectedVertices[below], colors[lat % colors.Length]);
                    DrawTriangle(g, projectedVertices[below], projectedVertices[next], projectedVertices[belowNext], colors[lat % colors.Length]);
                }
            }
        }

        private void DrawTriangle(Graphics g, Vector3 v1, Vector3 v2, Vector3 v3, Color color)
        {
            // Create points for the triangle
            Point[] points = new Point[] {
                new Point((int)v1.X, (int)v1.Y),
                new Point((int)v2.X, (int)v2.Y),
                new Point((int)v3.X, (int)v3.Y),
            };

            // Fill the triangle
            using (Brush brush = new SolidBrush(color))
            {
                g.FillPolygon(brush, points);
            }
        }

        public void ClearDepthBuffer()
        {
            for (int x = 0; x < Renderer.SCREEN_WIDTH; x++)
            {
                for (int y = 0; y < Renderer.SCREEN_HEIGHT; y++)
                {
                    depthBuffer[x, y] = float.MaxValue; // Set depth to maximum value
                }
            }
        }
    }
}
