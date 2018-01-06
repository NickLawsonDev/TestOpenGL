using OpenGLTest.RenderEngine;
using OpenTK;
using OpenTK.Graphics.ES20;
using System;

namespace OpenGLTest.EngineTester
{
    public class MainGameLoop
    {
        private const int _FPSCap = 120;
        private const int _width = 1280;
        private const int _height = 720;


        public MainGameLoop()
        {
            Init();
        }

        public void Init()
        {
            using (var _window = new DisplayManager(_width, _height))
            {
                _window.Run(_FPSCap);

            }
        }

        static void Main(string[] args)
        {
            var loop = new MainGameLoop();
        }
    }
}
