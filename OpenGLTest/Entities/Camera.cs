using OpenGLTest.Utility;
using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLTest.Entities
{
    public class Camera
    {
        public Vector3 Position { get; private set; } = new Vector3(0, 0, 0);
        public float Pitch { get; private set; } //high or low
        public float Yaw { get; private set; }   //left or right
        public float Roll { get; private set; }  //tilt

        public void Move()
        {
            var state = Keyboard.GetState();

            if (state.IsKeyDown(Key.W))
                Position = Position.Add(0, 0, -0.02f);
            else if (state.IsKeyDown(Key.S))
                Position = Position.Add(0, 0, 0.02f);
            else if(state.IsKeyDown(Key.D))
                Position = Position.Add(0.02f, 0, 0);
            else if (state.IsKeyDown(Key.A))
                Position = Position.Add(-0.02f, 0, 0);
            else if (state.IsKeyDown(Key.Z))
                Position = Position.Add(0, -0.02f, 0);
            else if (state.IsKeyDown(Key.X))
                Position = Position.Add(0, 0.02f, 0);
        }
    }
}
