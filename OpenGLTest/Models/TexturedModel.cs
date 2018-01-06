using OpenGLTest.RenderEngine;
using OpenGLTest.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLTest.Models
{
    public class TexturedModel
    {
        public RawModel RawModel { get; private set; }
        public ModelTexture Texture { get; private set; }

        public TexturedModel(RawModel model, ModelTexture texture)
        {
            RawModel = model;
            Texture = texture;
        }
    }
}
