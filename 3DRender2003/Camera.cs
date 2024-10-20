using System.Drawing;

namespace _DRender2003
{
    public class Camera
    {
        private Vector3 position;
        private Vector3 target;
        private float fieldOfView;

        public Camera(Vector3 position, Vector3 target, float fieldOfView)
        {
            this.position = position;
            this.target = target;
            this.fieldOfView = fieldOfView;
        }

        public Vector3 Project(Vector3 vertex)
        {
            // Adjust the vertex position based on the camera position
            vertex.X -= position.X;
            vertex.Y -= position.Y;
            vertex.Z -= position.Z;

            // Simple perspective projection logic
            float perspective = 400; // Focal length, adjust as needed
            if (vertex.Z <= 0) vertex.Z = 1; // Prevent division by zero

            float projectedX = (vertex.X * perspective) / (perspective + vertex.Z);
            float projectedY = (-vertex.Y * perspective) / (perspective + vertex.Z); // Invert Y coordinate

            // Return projected coordinates in screen space
            return new Vector3(projectedX + (Renderer.SCREEN_WIDTH / 2), projectedY + (Renderer.SCREEN_HEIGHT / 2), vertex.Z); // Center the projection
        }

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector3 Target
        {
            get { return target; }
            set { target = value; }
        }

        public float FieldOfView
        {
            get { return fieldOfView; }
            set { fieldOfView = value; }
        }
    }
}
