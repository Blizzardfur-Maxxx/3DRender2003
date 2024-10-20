namespace _DRender2003
{
    public class Vector3
    {
        // Backing fields for the properties
        private float x;
        private float y;
        private float z;

        // Public properties with backing fields
        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public float Z
        {
            get { return z; }
            set { z = value; }
        }

        // Properties for width, height, and depth
        public float Width
        {
            get { return x; }
            set { x = value; }
        }

        public float Height
        {
            get { return y; }
            set { y = value; }
        }

        public float Depth
        {
            get { return z; }
            set { z = value; }
        }

        // Constructor to initialize the vector
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3 Scale(float scaleFactor)
        {
            return new Vector3(X * scaleFactor, Y * scaleFactor, Z * scaleFactor);
        }

        // Constructor to create a size vector
        public Vector3(float width, float height) : this(width, height, 0) { }

        // Vector addition
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        // Vector subtraction
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3 operator /(Vector3 a, float scalar)
        {
            return new Vector3(a.X / scalar, a.Y / scalar, a.Z / scalar);
        }

        // Cross product
        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        // Dot product
        public static float Dot(Vector3 a, Vector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public Vector3 Normalize()
        {
            float length = (float)MathHelper.Sqrt(X * X + Y * Y + Z * Z);
            return new Vector3(X / length, Y / length, Z / length);
        }
    }
}
