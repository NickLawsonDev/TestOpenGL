using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGLTest.EngineTester;
using OpenGLTest.Entities;
using OpenGLTest.Models;
using OpenGLTest.Shaders;
using OpenGLTest.Textures;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenGLTest.RenderEngine
{
    public class DisplayManager : GameWindow
    {
        private StaticShader _shader;
        private ModelTexture _texture;
        private TexturedModel _texturedModel;
        private Entity _entity;
        private Camera _camera = new Camera();
        private Loader _loader = new Loader();
        private Light _light = new Light(new Vector3(0, 0, -20.0f), new Vector3(1,1,1));

        public DisplayManager(int width, int height) : base(width, height, GraphicsMode.Default, "OpenTK Guide", GameWindowFlags.Default, DisplayDevice.Default,
                    3,0,GraphicsContextFlags.ForwardCompatible)
        {
            VSync = VSyncMode.On;
            _shader = new StaticShader();
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0, 0, 1, 1);

            _texture = new ModelTexture(_loader.LoadTexture(@"Content\whitetexture.png"));
            var model = OBjLoader.LoadObjModel(@"Content\dragon.obj", _loader);
            _texturedModel = new TexturedModel(model, _texture);
            _entity = new Entity(_texturedModel, new Vector3(0, 0, -25.0f), 0, 0, 0, 1);

            Console.WriteLine("Running OpenGL version: " + GL.GetString(StringName.Version));
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Title = $"OpenTK Guide (Vsync: {VSync.ToString()}) FPS: {(1f / e.Time).ToString("0.")}";

            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //_entity.MoveEntity(new Vector3(0, 0, -0.2f));
            _entity.RotateEntity(0, 1f, 0);

            using (var _renderer = new Renderer(_shader))
            {
                _camera.Move();
                _shader.Start();
                _shader.LoadLight(_light);
                _shader.LoadViewMatrix(_camera);
                _renderer.Render(_entity, _shader);
                _shader.Stop();
            }

            SwapBuffers();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _shader.Dispose();
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
