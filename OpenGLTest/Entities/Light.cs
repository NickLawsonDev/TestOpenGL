using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLTest.Entities
{
    public class Light
    {
        public Light(Vector3 position, Vector3 color)
        {
            Position = position;
            Color = color;
        }

        public Vector3 Position { get; private set; }
        public Vector3 Color { get; private set; }
    }
}
