using OpenGLTest.Models;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLTest.Entities
{
    public class Entity
    {
        public TexturedModel Model { get; private set; }
        public Vector3 Position { get; private set; }
        public float RotX { get; private set; }
        public float RotY { get; private set; }
        public float RotZ { get; private set; }
        public float Scale { get; private set; }


        public Entity(TexturedModel model, Vector3 position, float rotX, float rotY, float rotZ, float scale)
        {
            Model = model;
            Position = position;
            RotX = rotX;
            RotY = rotY;
            RotZ = rotZ;
            Scale = scale;
        }

        public void MoveEntity(Vector3 movementVec)
        {
            Position = Vector3.Add(Position, movementVec);
        }

        public void RotateEntity(float dx, float dy, float dz)
        {
            RotX += dx;
            RotY += dy;
            RotZ += dz;
        }
    }
}
