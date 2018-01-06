using OpenGLTest.Entities;
using OpenGLTest.Utility;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLTest.Shaders
{
    public class StaticShader : ShaderProgram
    {
        private const string _VertexFile = @"Shaders\VertexShader.txt";
        private const string _FragmentFile = @"Shaders\FragmentShader.txt";

        private int _LocationTransformationMatrix;
        private int _LocationProjectionMatrix;
        private int _LocationViewMatrix;

        public StaticShader() : base(_VertexFile, _FragmentFile)
        {
        }

        protected override void BindAttributes()
        {
            BindAttribute(0, "position");
            BindAttribute(1, "textureCoords");
        }

        protected override void GetAllUniformLocations()
        {
            _LocationTransformationMatrix = GetUniformLocation("transformationMatrix");
            _LocationProjectionMatrix = GetUniformLocation("projectionMatrix");
            _LocationViewMatrix = GetUniformLocation("viewMatrix");
        }

        public void LoadTransformationMatrix(Matrix4 matrix)
        {
            LoadMatrix(_LocationTransformationMatrix, matrix);
        }

        public void LoadProjectionMatrix(Matrix4 matrix)
        {
            LoadMatrix(_LocationProjectionMatrix, matrix);
        }

        public void LoadViewMatrix(Camera camera)
        {
            LoadMatrix(_LocationViewMatrix, MathExtensions.CreateViewMatrix(camera));
        }
    }
}
