#if Hidden

using System;
using System.Drawing;
using Towel.Mathematics;
using Towel.DataStructures;
using OpenTK.Graphics.OpenGL;

namespace Towel.Graphics.OpenGL
{
    public class LoadedModel : Model
    {
        #region Nested Types

        public class VertexBufferObject
        {
            private int _id;
            private BufferTarget _bufferTarget;

            private VertexBufferObject(int vboId, BufferTarget type)
            {
                this._id = vboId;
                this._bufferTarget = type;
            }

            public int Id { get { return this._id; } }
            public BufferTarget BufferTarget { get { return this._bufferTarget; } }

            public static VertexBufferObject Create(BufferTarget type)
            {
                int id = GL.GenBuffer();
                return new VertexBufferObject(id, type);
            }

            public void Bind()
            {
                GL.BindBuffer(_bufferTarget, _id);
            }

            public void Unbind()
            {
                GL.BindBuffer(_bufferTarget, 0);
            }

            public void StoreData(float[] data)
            {
                GL.BufferData(_bufferTarget, data.Length, data, BufferUsageHint.StaticDraw);
            }

            public void StoreData(int[] data)
            {
                GL.BufferData(_bufferTarget, data.Length, data, BufferUsageHint.StaticDraw);
            }

            public void Delete()
            {
                GL.DeleteBuffer(_id);
            }
        }

        public class VertexArrayObject
        {
            private static int BYTES_PER_FLOAT = 4;
            private static int BYTES_PER_INT = 4;

            public int _id;
            public List<VertexBufferObject> _dataVbos = new ListArray<VertexBufferObject>();
            public VertexBufferObject _indexVbo;
            private int _indexCount;

            private VertexArrayObject(int id)
            {
                this._id = id;
            }

            public int Id { get { return this._id; } }
            public int IndexCount { get { return this._indexCount; } }

            public static VertexArrayObject Create()
            {
                int id = GL.GenVertexArray();
                return new VertexArrayObject(id);
            }

            public void Bind(params int[] attributes)
            {
                Bind();
                foreach (int i in attributes)
                {
                    GL.EnableVertexAttribArray(i);
                }
            }

            public void Unbind(params int[] attributes)
            {
                foreach (int i in attributes)
                {
                    GL.DisableVertexAttribArray(i);
                }
                Unbind();
            }

            public void CreateIndexBuffer(int[] indices)
            {
                this._indexVbo = VertexBufferObject.Create(BufferTarget.ElementArrayBuffer);
                _indexVbo.Bind();
                _indexVbo.StoreData(indices);
                this._indexCount = indices.Length;
            }

            public void CreateAttribute(int attribute, float[] data, int attrSize)
            {
                VertexBufferObject dataVbo = VertexBufferObject.Create(BufferTarget.ArrayBuffer);
                dataVbo.Bind();
                dataVbo.StoreData(data);
                GL.VertexAttribPointer(attribute, attrSize, VertexAttribPointerType.Float, false, attrSize * BYTES_PER_FLOAT, 0);
                dataVbo.Unbind();
                _dataVbos.Add(dataVbo);
            }

            public void CreateIntAttribute(int attribute, int[] data, int attrSize)
            {
                VertexBufferObject dataVbo = VertexBufferObject.Create(BufferTarget.ArrayBuffer);
                dataVbo.Bind();
                dataVbo.StoreData(data);
                GL.VertexAttribIPointer(attribute, attrSize, VertexAttribIntegerType.Int, attrSize * BYTES_PER_INT, System.IntPtr.Zero);

                dataVbo.Unbind();
                _dataVbos.Add(dataVbo);
            }

            public void Delete()
            {
                GL.DeleteVertexArray(_id);
                foreach (VertexBufferObject vbo in _dataVbos)
                {
                    vbo.Delete();
                }
                _indexVbo.Delete();
            }

            private void Bind()
            {
                GL.BindVertexArray(_id);
            }

            private void Unbind()
            {
                GL.BindVertexArray(0);
            }
        }

        #endregion

        private VertexArrayObject _vertexArrayObject;
        private int _textureId;
        
        public LoadedModel(Model model, VertexArrayObject vao, int textureId)
        {
            this._name = model._name;
            this._position = model._position;
            this._orientation = model._orientation;

            this._texture = null;// model._texture;

            this._positions = null;// model._positions;
            this._textureCoordinates = null;// model._textureCoordinates;
            this._normals = null;// model._normals;
            this._colorListformat = ColorListFormat.NotAvailable;// model._colorListformat;
            this._colors = null;// model._colors;
            this._jointIds = null;// model._jointIds;
            this._jointWeights = null;// model._jointWeights;
            this._indexListFormat = model._indexListFormat;
            this._indices = null;// model._indices;
            
            this._joints = model._joints;
            this._animations = model._animations;

            this._vertexArrayObject = vao;
            this._textureId = textureId;
        }

        public VertexArrayObject LoadedVertexArrayObject { get { return this._vertexArrayObject; } }
        public int LoadedTextureId { get { return this._textureId; } }
    }
}

#endif
