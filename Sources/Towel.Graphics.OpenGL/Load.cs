#if Hidden

using System.Drawing;
using System.Drawing.Imaging;

using OpenTK.Graphics.OpenGL;

namespace Towel.Graphics.OpenGL
{
    public static class Load
    {
        public static int Image(Bitmap image)
        {
            int texID = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, texID);
            BitmapData data = image.LockBits(new System.Drawing.Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            image.UnlockBits(data);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return texID;
        }

        public static LoadedModel Model(Model model)
        {
            int textureId = Load.Image(model._texture);

            LoadedModel.VertexArrayObject vao = LoadedModel.VertexArrayObject.Create();
            vao.Bind();
            vao.CreateIndexBuffer(model._indices);
            vao.CreateAttribute(0, model._positions, 3);
            vao.CreateAttribute(1, model._textureCoordinates, 2);
            vao.CreateAttribute(2, model._normals, 3);
            vao.CreateIntAttribute(3, model._jointIds, 3);
            vao.CreateAttribute(4, model._jointWeights, 3);
            vao.Unbind();
            
            return new LoadedModel(model, vao, textureId);
        }
    }
}

#endif
