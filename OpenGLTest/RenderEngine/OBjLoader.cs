using OpenGLTest.RenderEngine;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLTest.EngineTester
{
    public class OBjLoader
    {
        public static RawModel LoadObjModel(string fileName, Loader loader)
        {
            if (File.Exists(fileName))
            {
                var objFile = File.ReadAllText(fileName);
                List<Vector3> vertices = new List<Vector3>();
                List<Vector2> textures = new List<Vector2>();
                List<Vector3> normals = new List<Vector3>();
                List<int> indices = new List<int>();
                float[] verticesArray = null;
                float[] normalsArray = null;
                float[] texturesArray = null;
                int[] indicesArray = null;

                string line;
                StreamReader file = new StreamReader(fileName);

                try
                {
                    while(true)
                    {
                        line = file.ReadLine();
                        string[] currentLine = line.Split(' ');
                        if (line.StartsWith("v "))
                        {
                            Vector3 vertex = new Vector3(float.Parse(currentLine[1]), float.Parse(currentLine[2]), float.Parse(currentLine[3]));
                            vertices.Add(vertex);
                        }
                        else if (line.StartsWith("vt "))
                        {
                            Vector2 texture = new Vector2(float.Parse(currentLine[1]), float.Parse(currentLine[2]));
                            textures.Add(texture);
                        }
                        else if (line.StartsWith("vn "))
                        {
                            Vector3 normal = new Vector3(float.Parse(currentLine[1]), float.Parse(currentLine[2]), float.Parse(currentLine[3]));
                            normals.Add(normal);
                        }
                        else if (line.StartsWith("f "))
                        {
                            texturesArray = new float[vertices.Count * 2];
                            normalsArray = new float[vertices.Count * 3];
                            break;
                        }
                    }

                    while(line != null)
                    {
                        if (!line.StartsWith("f "))
                        {
                            line = file.ReadLine();
                            continue;
                        }

                        string[] currentLine = line.Split(' ');
                        string[] vertex1 = currentLine[1].Split('/');
                        string[] vertex2 = currentLine[2].Split('/');
                        string[] vertex3 = currentLine[3].Split('/');

                        ProcessVertex(vertex1, indices, textures, normals, texturesArray, normalsArray);
                        ProcessVertex(vertex2, indices, textures, normals, texturesArray, normalsArray);
                        ProcessVertex(vertex3, indices, textures, normals, texturesArray, normalsArray);
                        line = file.ReadLine();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                verticesArray = new float[vertices.Count * 3];
                indicesArray = new int[indices.Count];

                int vertexPointer = 0;
                foreach(var vertex in vertices)
                {
                    verticesArray[vertexPointer++] = vertex.X;
                    verticesArray[vertexPointer++] = vertex.Y;
                    verticesArray[vertexPointer++] = vertex.Z;
                }

                for(var i = 0; i < indices.Count; i++)
                {
                    indicesArray[i] = indices[i];
                }

                return loader.LoadToVAO(verticesArray, indicesArray, texturesArray, normalsArray);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        private static void ProcessVertex(string[] vertexData, List<int> indices, List<Vector2> textures, List<Vector3> normals, float[] textureArray, float[] normalArray)
        {
            int currentVertexPointer = int.Parse(vertexData[0]) - 1;
            indices.Add(currentVertexPointer);
            Vector2 currentTex = textures[int.Parse(vertexData[1]) - 1];
            textureArray[currentVertexPointer * 2] = currentTex.X;
            textureArray[currentVertexPointer * 2 + 1] = 1 - currentTex.Y;
            Vector3 currentNorm = normals[int.Parse(vertexData[2]) - 1];
            normalArray[currentVertexPointer * 3] = currentNorm.X;
            normalArray[currentVertexPointer * 3+1] = currentNorm.Y;
            normalArray[currentVertexPointer * 3+2] = currentNorm.Z;
        }
    }
}
