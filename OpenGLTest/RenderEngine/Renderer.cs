using OpenGLTest.Entities;
using OpenGLTest.Models;
using OpenGLTest.Shaders;
using OpenGLTest.Utility;
using OpenTK;
using OpenTK.Graphics.ES30;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLTest.RenderEngine
{
    public class Renderer : IDisposable
    {
        private const float _FOV = 90f;
        private const float _nearPlane = 0.1f;
        private const float _farPlane = 100.0f;

        private Matrix4 projectionMatrix;

        public Renderer(StaticShader shader)
        {
            CreateProjectionMatrix();
            shader.Start();
            shader.LoadProjectionMatrix(projectionMatrix);
            shader.Stop();
        }

        public void Render(Entity entity, StaticShader shader)
        {
            GL.BindVertexArray(entity.Model.RawModel.VAOID);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            var transformationMatrix = MathExtensions.CreateTransformationMatrix(entity.Position, entity.RotX, entity.RotY, entity.RotZ, entity.Scale);
            shader.LoadTransformationMatrix(transformationMatrix);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, entity.Model.Texture.TextureID);
            GL.DrawElements(PrimitiveType.Triangles, entity.Model.RawModel.VertexCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.BindVertexArray(0);
        }

        private void CreateProjectionMatrix()
        {
            float aspectRatio = 1280/ (float)720;
            float fovy = (float)Math.PI * (_FOV / 180f);

            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(fovy, aspectRatio, _nearPlane, _farPlane);
        }

        public void Dispose()
        {
        }
    }
}
