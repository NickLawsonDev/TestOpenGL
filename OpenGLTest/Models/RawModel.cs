using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLTest.RenderEngine
{
    public class RawModel
    {
        public int VAOID { get; private set; }
        public int VertexCount { get; private set; }

        public RawModel(int vaoID, int vertexCount)
        {
            VAOID = vaoID;
            VertexCount = vertexCount;
        }
    }
}
