using System;

namespace _DRender2003
{
    public class Entity
    {
        private Vector3 position;
        private Vector3 rotation;
        private Vector3 scale;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector3 Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Vector3 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public Entity(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }

        public void Move(Vector3 translation)
        {
            Position += translation;
        }

        public void Rotate(Vector3 rotationDelta)
        {
            Rotation += rotationDelta;
        }

        public void SetRotate(Vector3 scaleDelta)
        {
            Scale += scaleDelta;
        }

        public void SetScale(Vector3 newScale)
        {
            Scale = newScale;
        }

        public Vector3 ApplyRotation(Vector3 vertex)
        {
            return RotateX(RotateY(RotateZ(vertex)));
        }

        private Vector3 RotateX(Vector3 vertex)
        {
            float radians = MathHelper.ToRadians(Rotation.X);
            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);
            float y = vertex.Y * cos - vertex.Z * sin;
            float z = vertex.Y * sin + vertex.Z * cos;
            return new Vector3(vertex.X, y, z);
        }

        private Vector3 RotateY(Vector3 vertex)
        {
            float radians = MathHelper.ToRadians(Rotation.Y);
            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);
            float x = vertex.X * cos + vertex.Z * sin;
            float z = -vertex.X * sin + vertex.Z * cos;
            return new Vector3(x, vertex.Y, z);
        }

        private Vector3 RotateZ(Vector3 vertex)
        {
            float radians = MathHelper.ToRadians(Rotation.Z);
            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);
            float x = vertex.X * cos - vertex.Y * sin;
            float y = vertex.X * sin + vertex.Y * cos;
            return new Vector3(x, y, vertex.Z);
        }
    }
}
