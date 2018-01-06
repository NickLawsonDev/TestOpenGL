using OpenGLTest.Entities;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLTest.Utility
{
    public static class MathExtensions
    {
        public static Matrix4 CreateTransformationMatrix(Vector3 translation, float rx, float ry, float rz, float scale)
        {
            var translationMatrix = Matrix4.CreateTranslation(translation);
            var rotX = Matrix4.CreateFromAxisAngle(new Vector3(1, 0, 0), MathHelper.DegreesToRadians(rx));
            var rotY = Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), MathHelper.DegreesToRadians(ry));
            var rotZ = Matrix4.CreateFromAxisAngle(new Vector3(0, 0, 1), MathHelper.DegreesToRadians(rz));
            var scaleMatrix = Matrix4.CreateScale(scale);

            return scaleMatrix * (rotX * rotY * rotZ) * translationMatrix;
        }

        public static Matrix4 CreateViewMatrix(Camera camera)
        {
            var rotX = Matrix4.CreateFromAxisAngle(new Vector3(1, 0, 0), MathHelper.DegreesToRadians(camera.Pitch));
            var rotY = Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), MathHelper.DegreesToRadians(camera.Yaw));
            var negativeCameraPos = new Vector3(-camera.Position.X, -camera.Position.Y, -camera.Position.Z);
            var translationMatrix = Matrix4.CreateTranslation(negativeCameraPos);

            return (rotX * rotY) * translationMatrix;
        }

        public static Vector3 Change(this Vector3 org, object x = null, object y = null, object z = null)
        {
            float newX;
            float newY;
            float newZ;

            if (x == null)
                newX = org.X;
            else
                newX = (float)x;

            if (y == null)
                newY = org.Y;
            else
                newY = (float)y;

            if (z == null)
                newZ = org.Z;
            else
                newZ = (float)z;

            return new Vector3(newX, newY, newZ);
        }

        public static Vector3 Add(this Vector3 org, float x = 0, float y = 0, float z = 0)
        {
            return new Vector3(x == 0 ? org.X : org.X += x, y == 0 ? org.Y : org.Y += y, z == 0 ? org.Z : org.Z += z);
        }
    }
}
