using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLTest.RenderEngine
{
    public class Loader : IDisposable
    {
        private List<int> _vaos = new List<int>();
        private List<int> _vbos = new List<int>();
        private List<int> _textures = new List<int>();

        public RawModel LoadToVAO(float[] positions, int[] indices, float[] textureCoords)
        {
            var vaoID = CreateVAO();
            BindIndicesBuffer(indices);
            StoreDataInAttributeList(0, 3, positions);
            StoreDataInAttributeList(1, 2, textureCoords);
            UnbindVAO();
            return new RawModel(vaoID, indices.Length);
        }

        private int LoadTexture(Bitmap image)
        {
            int texID = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, texID);
            BitmapData data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            image.UnlockBits(data);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            _textures.Add(texID);
            return texID;
        }

        public int LoadTexture(string file)
        {
            try
            {
                var bitmap = new Bitmap(file);
                return LoadTexture(bitmap);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        private int CreateVAO()
        {
            var vaoID = GL.GenVertexArray();
            _vaos.Add(vaoID);
            GL.BindVertexArray(vaoID);
            return vaoID;
        }

        private void StoreDataInAttributeList(int attributeNumber, int coordinateSize, float[] data)
        {
            int vboID;
            GL.GenBuffers(1, out vboID);
            _vbos.Add(vboID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, GenerateIntPtr(data), data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attributeNumber, coordinateSize, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void CleanUp()
        {
            foreach(var vao in _vaos)
            {
                GL.DeleteVertexArray(vao);
            }

            foreach (var vbo in _vbos)
            {
                GL.DeleteBuffer(vbo);
            }

            foreach (var tex in _textures)
            {
                GL.DeleteTexture(tex);
            }
        }

        private void UnbindVAO()
        {
            GL.BindVertexArray(0);
        }

        private void BindIndicesBuffer(int[] indices)
        {
            int vboID;
            GL.GenBuffers(1, out vboID);
            _vbos.Add(vboID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, GenerateIntPtr(indices), indices, BufferUsageHint.StaticDraw);
        }

        private IntPtr GenerateIntPtr(int[] data)
        {
            return (IntPtr)(data.Length * sizeof(int));
        }

        private IntPtr GenerateIntPtr(float[] data)
        {
            return (IntPtr)(data.Length * sizeof(float));
        }

        public void Dispose()
        {
            CleanUp();
        }
    }
}
