using OpenTK;
using OpenTK.Graphics.ES20;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLTest.Shaders
{
    public abstract class ShaderProgram : IDisposable
    {
        private int _ProgramID;
        private int _VertexShaderID;
        private int _FragmentShaderID;

        public ShaderProgram(string vertexFile, string fragmentFile)
        {
            _VertexShaderID = LoadShader(vertexFile, ShaderType.VertexShader);
            _FragmentShaderID = LoadShader(fragmentFile, ShaderType.FragmentShader);
            _ProgramID = GL.CreateProgram();
            GL.AttachShader(_ProgramID, _VertexShaderID);
            GL.AttachShader(_ProgramID, _FragmentShaderID);
            BindAttributes();
            GL.LinkProgram(_ProgramID);
            GL.ValidateProgram(_ProgramID);
            GetAllUniformLocations();
        }

        public void Start()
        {
            GL.UseProgram(_ProgramID);
        }

        public void Stop()
        {
            GL.UseProgram(0);
        }

        protected int GetUniformLocation(string uniformName)
        {
            return GL.GetUniformLocation(_ProgramID, uniformName);
        }

        protected abstract void GetAllUniformLocations();

        public void Dispose()
        {
            Stop();
            GL.DetachShader(_ProgramID, _VertexShaderID);
            GL.DetachShader(_ProgramID, _FragmentShaderID);
            GL.DeleteShader(_VertexShaderID);
            GL.DeleteShader(_FragmentShaderID);
            GL.DeleteProgram(_ProgramID);
        }

        protected abstract void BindAttributes();

        protected void BindAttribute(int attribute, string variableName)
        {
            GL.BindAttribLocation(_ProgramID, attribute, variableName);
        }

        protected void LoadFloat(int location, float value)
        {
            GL.Uniform1(location, value);
        }

        protected void LoadVector(int location, Vector3 vector)
        {
            GL.Uniform3(location, vector);
        }

        protected void LoadBool(int location, bool value)
        {
            var boolValue = 0;
            if (value)
                boolValue = 1;

            GL.Uniform1(location, boolValue);
        }

        protected void LoadMatrix(int location, Matrix4 matrix)
        {
            GL.UniformMatrix4(location, false, ref matrix);
        }

        private static int LoadShader(string file, ShaderType type)
        {
            string shaderSource;

            if(File.Exists(file))
            {
                shaderSource = File.ReadAllText(file);

                var shaderID = GL.CreateShader(type);
                GL.ShaderSource(shaderID, shaderSource);
                GL.CompileShader(shaderID);

                return shaderID;
            }

            return 0;
        }
    }
}
