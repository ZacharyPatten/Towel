using System;
using System.Globalization;
using System.IO;

using Towel.DataStructures;

namespace Towel.Graphics.Formats
{
    /// <summary>Wavefront OBJ (.obj) file format operations.</summary>
    public class Obj
    {
        /// <summary>Parses an OBJ file into a model instance.</summary>
        /// <param name="objFileContents">The contents of the obj file.</param>
        /// <returns>The parsed model.</returns>
        private static Model Parse(string objFileContents)
        {
            // These are temporarily needed lists for storing the parsed data as you read it.
            // Its better to use "ListArrays" vs "Lists" because they will be accessed by indeces
            // by the faces of the obj file.
            ListArray<float> filePositions = new ListArray<float>();
            ListArray<float> fileNormals = new ListArray<float>();
            ListArray<float> fileTextureCoordinates = new ListArray<float>();
            ListArray<int> fileIndeces = new ListArray<int>();

            // Obj files are not required to include texture coordinates or normals
            bool hasTextureCoordinates = true;
            bool hasNormals = true;
            
            // Lets read the file and handle each line separately for ".obj" files
            using (StringReader reader = new StringReader(objFileContents))
            {
                int lineNumber = 1;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    try
                    {
                        string[] parameters = line.Trim().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        switch (parameters[0])
                        {
                            // Vertex
                            case "v":
                                filePositions.Add(float.Parse(parameters[1], CultureInfo.InvariantCulture));
                                filePositions.Add(float.Parse(parameters[2], CultureInfo.InvariantCulture));
                                filePositions.Add(float.Parse(parameters[3], CultureInfo.InvariantCulture));
                                break;

                            // Texture Coordinate
                            case "vt":
                                fileTextureCoordinates.Add(float.Parse(parameters[1], CultureInfo.InvariantCulture));
                                fileTextureCoordinates.Add(float.Parse(parameters[2], CultureInfo.InvariantCulture));
                                break;

                            // Normal
                            case "vn":
                                fileNormals.Add(float.Parse(parameters[1], CultureInfo.InvariantCulture));
                                fileNormals.Add(float.Parse(parameters[2], CultureInfo.InvariantCulture));
                                fileNormals.Add(float.Parse(parameters[3], CultureInfo.InvariantCulture));
                                break;

                            // Face
                            case "f":
                                //if (parameters.Length < 4)
                                //  throw new StaticModelManagerException("obj file corrupt.");
                                int first = fileIndeces.Count;
                                for (int i = 1; i < parameters.Length; i++)
                                {
                                    if (i > 3)
                                    {
                                        // Triangulate using the previous two verteces
                                        // NOTE: THIS MAY BE INCORRECT! I COULD NOT YET FIND DOCUMENTATION
                                        // ON THE TRIANGULATION DONE BY BLENDER (WORKS FOR QUADS AT LEAST)

                                        //// Last two (triangle strip)
                                        //fileIndeces.Add(fileIndeces[fileIndeces.Count - 6]);
                                        //fileIndeces.Add(fileIndeces[fileIndeces.Count - 6]);
                                        //fileIndeces.Add(fileIndeces[fileIndeces.Count - 6]);
                                        //fileIndeces.Add(fileIndeces[fileIndeces.Count - 6]);
                                        //fileIndeces.Add(fileIndeces[fileIndeces.Count - 6]);
                                        //fileIndeces.Add(fileIndeces[fileIndeces.Count - 6]);

                                        // First then previous (triangle fan)
                                        fileIndeces.Add(fileIndeces[first]);
                                        fileIndeces.Add(fileIndeces[first + 1]);
                                        fileIndeces.Add(fileIndeces[first + 2]);
                                        fileIndeces.Add(fileIndeces[fileIndeces.Count - 6]);
                                        fileIndeces.Add(fileIndeces[fileIndeces.Count - 6]);
                                        fileIndeces.Add(fileIndeces[fileIndeces.Count - 6]);
                                    }

                                    // Now include the new vertex
                                    string[] indexReferences = parameters[i].Split('/');
                                    //if (indexReferences[0] == "")
                                    //  throw new StaticModelManagerException("ERROR: obj file corrupted (missing vertex possition):" + filePath);
                                    fileIndeces.Add(int.Parse(indexReferences[0], CultureInfo.InvariantCulture));

                                    if (hasNormals && indexReferences.Length < 3)
                                        hasNormals = false;
                                    if (hasTextureCoordinates && (indexReferences.Length < 2 || indexReferences[1] == ""))
                                        hasTextureCoordinates = false;

                                    if (hasTextureCoordinates && indexReferences[1] != "")
                                        fileIndeces.Add(int.Parse(indexReferences[1], CultureInfo.InvariantCulture));
                                    else
                                        fileIndeces.Add(0);

                                    if (hasNormals && indexReferences[2] != "")
                                        fileIndeces.Add(int.Parse(indexReferences[2], CultureInfo.InvariantCulture));
                                    else
                                        fileIndeces.Add(0);
                                }
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new System.FormatException("Could not parse OBJ file.", ex);
                    }
                    lineNumber++;
                }
            }

            // Pull the final vertex order out of the indexed references
            // Note, arrays start at 0 but the index references start at 1
            float[] positions = new float[fileIndeces.Count];
            for (int i = 0; i < fileIndeces.Count; i += 3)
            {
                int index = (fileIndeces[i] - 1) * 3;
                positions[i] = filePositions[index];
                positions[i + 1] = filePositions[index + 1];
                positions[i + 2] = filePositions[index + 2];
            }

            float[] textureCoordinates = null;
            if (hasTextureCoordinates)
            {
                // Pull the final texture coordinates order out of the indexed references
                // Note, arrays start at 0 but the index references start at 1
                // Note, every other value needs to be inversed (not sure why but it works :P)
                textureCoordinates = new float[fileIndeces.Count / 3 * 2];
                for (int i = 1; i < fileIndeces.Count; i += 3)
                {
                    int index = (fileIndeces[i] - 1) * 2;
                    int offset = (i - 1) / 3;
                    textureCoordinates[i - 1 - offset] = fileTextureCoordinates[index];
                    textureCoordinates[i - offset] = 1 - fileTextureCoordinates[(index + 1)];
                }
            }

            float[] normals = null;
            if (hasNormals)
            {
                // Pull the final normal order out of the indexed references
                // Note, arrays start at 0 but the index references start at 1
                normals = new float[fileIndeces.Count];
                for (int i = 2; i < fileIndeces.Count; i += 3)
                {
                    int index = (fileIndeces[i] - 1) * 3;
                    normals[i - 2] = fileNormals[index];
                    normals[i - 1] = fileNormals[(index + 1)];
                    normals[i] = fileNormals[(index + 2)];
                }
            }

            int[] indeces = new int[positions.Length / 3];
            for (int i = 0; i < indeces.Length; i++)
            {
                indeces[i] = i;
            }

            return new Model()
            {
                _positions = positions,
                _normals = normals,
                _textureCoordinates = textureCoordinates,
                _indices = indeces,
                _indexListFormat = Model.IndexListFormat.Triangles,
            };
        }
    }
}
