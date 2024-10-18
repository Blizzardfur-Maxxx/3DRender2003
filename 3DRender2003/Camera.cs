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
            float perspective = 400; // Focal length
            float projectedX = vertex.X * perspective / (perspective + vertex.Z);
            float projectedY = vertex.Y * perspective / (perspective + vertex.Z);

            // Return projected coordinates
            return new Vector3(projectedX, projectedY, 0);
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
