using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLTest.Textures
{
    public class ModelTexture
    {
        public int TextureID { get; private set; }

        public ModelTexture(int id)
        {
            TextureID = id;
        }
    }
}
