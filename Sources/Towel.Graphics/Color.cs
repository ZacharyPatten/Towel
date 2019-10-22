using Towel;

using System;
using Towel.Mathematics;
using static Towel.Syntax;

namespace Towel.Graphics
{
    /// <summary>
    /// Standard color class for the Theta Graphics library. Contains Red, Green, Blue and Alpha (transparency)
    /// values expressed as 32 bit floating points (0-1).
    /// </summary>
	public struct Color
	{
		private float _r;
		private float _g;
		private float _b;
		private float _a;

        private Color(float red, float green, float blue, float alpha)
		{
			this._r = red;
			this._g = green;
			this._b = blue;
			this._a = alpha;
		}

        #region Properties

        /// <summary>The amount of red in the color (0-1).</summary>
        public float R
        {
            get { return this._r; }
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, "!(0 <= " + nameof(value) + " <= 1)");
                }
                this._r = value;
            }
        }

        /// <summary>The amount of green in the color (0-1).</summary>
        public float G
        {
            get { return this._g; }
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, "!(0 <= " + nameof(value) + " <= 1)");
                }
                this._g = value;
            }
        }

        /// <summary>The amount of blue in the color (0-1).</summary>
        public float B
        {
            get { return this._b; }
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, "!(0 <= " + nameof(value) + " <= 1)");
                }
                this._b = value;
            }
        }

        /// <summary>The amount of alpha (transparency) in the color (0-1).</summary>
        public float A
        {
            get { return this._a; }
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, "!(0 <= " + nameof(value) + " <= 1)");
                }
                this._a = value;
            }
        }

        #endregion

        #region Operations (non-conversion)

        public static Color operator +(Color a, Color b)
        {
            return Color.Add(a, b);
        }

        public static Color Add(Color a, Color b)
        {
            return new Color(
                Maximum(a.R + b.R, 1f),
                Maximum(a.G + b.G, 1f),
                Maximum(a.B + b.B, 1f),
                Maximum(a.A + b.A, 1f));
        }

        public static Color operator -(Color a, Color b)
        {
            return Color.Subtract(a, b);
        }

        public static Color Subtract(Color a, Color b)
        {
            return new Color(
                Maximum(a.R - b.R, 0f),
                Maximum(a.G - b.G, 0f),
                Maximum(a.B - b.B, 0f),
                Maximum(a.A - b.A, 0f));
        }

        public static bool operator ==(Color a, Color b)
        {
            return Color.Equals(a, b);
        }

        public static bool operator !=(Color a, Color b)
        {
            return !Color.Equals(a, b);
        }

        public static bool Equals(Color a, Color b)
        {
            return
                a.R == b.R &&
                a.G == b.G &&
                a.B == b.B &&
                a.A == b.A;
        }

        public static Color Invert(Color color)
        {
            return new Color(
                1f - color.R,
                1f - color.G,
                1f - color.B,
                1f - color.A);
        }

        public Color Invert()
        {
            return Color.Invert(this);
        }

        #endregion

        #region Color Format Conversions

        #region sRGB

        /// <summary>Converts sRGB color values to Towel.Graphics.Color values.</summary>
        /// <param name="red">The sRGB red component.</param>
        /// <param name="green">The sRGB green component.</param>
        /// <param name="blue">The sRGB blue component.</param>
        /// <param name="alpha">The sRGB alpha component.</param>
        /// <returns>The converted color value.</returns>
        /// <citation>
        /// This conversion algorithm was originally developed as part of the
        /// OpenTK open source project. Hoever, it has been modified since its
        /// addition to the Theta Framework.
        /// </citation>
        public static Color FromSrgb(float red, float green, float blue, float alpha)
        {
            float r, g, b;

            if (red <= 0.04045f)
            {
                r = red / 12.92f;
            }
            else
            {
                r = (float)Power((red + 0.055f) / (1.0f + 0.055f), 2.4f);
            }

            if (green <= 0.04045f)
            {
                g = green / 12.92f;
            }
            else
            {
                g = (float)Power((green + 0.055f) / (1.0f + 0.055f), 2.4f);
            }

            if (blue <= 0.04045f)
            {
                b = blue / 12.92f;
            }
            else
            {
                b = (float)Power((blue + 0.055f) / (1.0f + 0.055f), 2.4f);
            }

            return new Color(r, g, b, alpha);
        }



        /// <summary>Converts Towel.Graphics.Color values sRGB values.</summary>
        /// <param name="color">The Towel.Graphics.Color to convert.</param>
        /// <param name="red">The sRGB red component.</param>
        /// <param name="green">The sRGB green component.</param>
        /// <param name="blue">The sRGB blue component.</param>
        /// <param name="alpha">The sRGB alpha component.</param>
        /// <citation>
        /// This conversion algorithm was originally developed as part of the
        /// OpenTK open source project. Hoever, it has been modified since its
        /// addition to the Theta Framework.
        /// </citation>
        public static void ToSrgb(Color color, out float red, out float green, out float blue, out float alpha)
        {
            if (color.R <= 0.0031308)
            {
                red = 12.92f * color.R;
            }
            else
            {
                red = (1.0f + 0.055f) * (float)Power(color.R, 1.0f / 2.4f) - 0.055f;
            }

            if (color.G <= 0.0031308)
            {
                green = 12.92f * color.G;
            }
            else
            {
                green = (1.0f + 0.055f) * (float)Power(color.G, 1.0f / 2.4f) - 0.055f;
            }

            if (color.B <= 0.0031308)
            {
                blue = 12.92f * color.B;
            }
            else
            {
                blue = (1.0f + 0.055f) * (float)Power(color.B, 1.0f / 2.4f) - 0.055f;
            }

            alpha = color.A;
        }

        /// <summary>Converts Towel.Graphics.Color values sRGB values.</summary>
        /// <param name="red">The sRGB red component.</param>
        /// <param name="green">The sRGB green component.</param>
        /// <param name="blue">The sRGB blue component.</param>
        /// <param name="alpha">The sRGB alpha component.</param>
        public void ToSrgb(out float red, out float green, out float blue, out float alpha)
        {
            Color.ToSrgb(this, out red, out green, out blue, out alpha);
        }

        #endregion

        #region HSL

        /// <summary>Converts HSL color values to Towel.Graphics.Color values.</summary>
        /// <param name="hue">The HSL hue component.</param>
        /// <param name="saturation">The HSL hue saturation.</param>
        /// <param name="lightness">The HSL hue lightness.</param>
        /// <param name="alpha">The HSL hue alpha.</param>
        /// <returns>The converted color value.</returns>
        /// <citation>
        /// This conversion algorithm was originally developed as part of the
        /// OpenTK open source project. Hoever, it has been modified since its
        /// addition to the Theta Framework.
        /// </citation>
        public static Color FromHsl(float hue, float saturation, float lightness, float alpha)
        {
            hue = hue * 360.0f;

            var C = (1.0f - AbsoluteValue(2.0f * lightness - 1.0f)) * saturation;

            var h = hue / 60.0f;
            var X = C * (1.0f - AbsoluteValue(h % 2.0f - 1.0f));

            float r, g, b;
            if (0.0f <= h && h < 1.0f)
            {
                r = C;
                g = X;
                b = 0.0f;
            }
            else if (1.0f <= h && h < 2.0f)
            {
                r = X;
                g = C;
                b = 0.0f;
            }
            else if (2.0f <= h && h < 3.0f)
            {
                r = 0.0f;
                g = C;
                b = X;
            }
            else if (3.0f <= h && h < 4.0f)
            {
                r = 0.0f;
                g = X;
                b = C;
            }
            else if (4.0f <= h && h < 5.0f)
            {
                r = X;
                g = 0.0f;
                b = C;
            }
            else if (5.0f <= h && h < 6.0f)
            {
                r = C;
                g = 0.0f;
                b = X;
            }
            else
            {
                r = 0.0f;
                g = 0.0f;
                b = 0.0f;
            }

            var m = lightness - (C / 2.0f);
            return new Color(r + m, g + m, b + m, alpha);
        }

        /// <summary>Converts Towel.Graphics.Color value to HSL color values.</summary>
        /// <param name="color">The Towel.Graphics.Color value to convert.</param>
        /// <param name="hue">The HSL hue component.</param>
        /// <param name="saturation">The HSL hue saturation.</param>
        /// <param name="lightness">The HSL hue lightness.</param>
        /// <param name="alpha">The HSL hue alpha.</param>
        /// <citation>
        /// This conversion algorithm was originally developed as part of the
        /// OpenTK open source project. Hoever, it has been modified since its
        /// addition to the Theta Framework.
        /// </citation>
        public static void ToHsl(Color color, out float hue, out float saturation, out float lightness, out float alpha)
        {
            var M = Maximum(color.R, Maximum(color.G, color.B));
            var m = Minimum(color.R, Minimum(color.G, color.B));
            var C = M - m;

            float h = 0.0f;
            if (M == color.R)
            {
                h = ((color.G - color.B) / C);
            }
            else if (M == color.G)
            {
                h = ((color.B - color.R) / C) + 2.0f;
            }
            else if (M == color.B)
            {
                h = ((color.R - color.G) / C) + 4.0f;
            }

            hue = h / 6.0f;
            if (hue < 0.0f)
            {
                hue += 1.0f;
            }

            lightness = (M + m) / 2.0f;

            saturation = 0.0f;
            if (0.0f != lightness && lightness != 1.0f)
            {
                saturation = C / (1.0f - Math.Abs(2.0f * lightness - 1.0f));
            }

            alpha = color.A;
        }

        /// <summary>Converts Towel.Graphics.Color value to HSL color values.</summary>
        /// <param name="hue">The HSL hue component.</param>
        /// <param name="saturation">The HSL hue saturation.</param>
        /// <param name="lightness">The HSL hue lightness.</param>
        /// <param name="alpha">The HSL hue alpha.</param>
        public void ToHsl(out float hue, out float saturation, out float lightness, out float alpha)
        {
            Color.ToHsl(this, out hue, out saturation, out lightness, out alpha);
        }

        #endregion

        #region HSV

        /// <summary>Converts HSV color values to Towel.Graphics.Color values.</summary>
        /// <param name="hue">The HSV hue component.</param>
        /// <param name="saturation">The HSV saturation component.</param>
        /// <param name="value">The HSV value component.</param>
        /// <param name="alpha">The HSV alpha component.</param>
        /// <returns>The converted color value.</returns>
        /// <citation>
        /// This conversion algorithm was originally developed as part of the
        /// OpenTK open source project. Hoever, it has been modified since its
        /// addition to the Theta Framework.
        /// </citation>
        public static Color FromHsv(float hue, float saturation, float value, float alpha)
        {
            hue = hue * 360.0f;

            var C = value * saturation;

            var h = hue / 60.0f;
            var X = C * (1.0f - Math.Abs(h % 2.0f - 1.0f));

            float r, g, b;
            if (0.0f <= h && h < 1.0f)
            {
                r = C;
                g = X;
                b = 0.0f;
            }
            else if (1.0f <= h && h < 2.0f)
            {
                r = X;
                g = C;
                b = 0.0f;
            }
            else if (2.0f <= h && h < 3.0f)
            {
                r = 0.0f;
                g = C;
                b = X;
            }
            else if (3.0f <= h && h < 4.0f)
            {
                r = 0.0f;
                g = X;
                b = C;
            }
            else if (4.0f <= h && h < 5.0f)
            {
                r = X;
                g = 0.0f;
                b = C;
            }
            else if (5.0f <= h && h < 6.0f)
            {
                r = C;
                g = 0.0f;
                b = X;
            }
            else
            {
                r = 0.0f;
                g = 0.0f;
                b = 0.0f;
            }

            var m = value - C;
            return new Color(r + m, g + m, b + m, alpha);
        }

        /// <summary>Converts Towel.Graphics.Color values to HSV color values.</summary>
        /// <param name="color">The Towel.Graphics.Color value to convert.</param>
        /// <param name="hue">The HSV hue component.</param>
        /// <param name="saturation">The HSV saturation component.</param>
        /// <param name="value">The HSV value component.</param>
        /// <param name="alpha">The HSV alpha component.</param>
        /// <citation>
        /// This conversion algorithm was originally developed as part of the
        /// OpenTK open source project. Hoever, it has been modified since its
        /// addition to the Theta Framework.
        /// </citation>
        public static void ToHsv(Color color, out float hue, out float saturation, out float value, out float alpha)
        {
            var M = Math.Max(color.R, Math.Max(color.G, color.B));
            var m = Math.Min(color.R, Math.Min(color.G, color.B));
            var C = M - m;
            float h = 0.0f;
            if (M == color.R)
            {
                h = ((color.G - color.B) / C) % 6.0f;
            }
            else if (M == color.G)
            {
                h = ((color.B - color.R) / C) + 2.0f;
            }
            else if (M == color.B)
            {
                h = ((color.R - color.G) / C) + 4.0f;
            }
            hue = (h * 60.0f) / 360.0f;
            saturation = 0.0f;
            if (0.0f != M)
            {
                saturation = C / M;
            }
            value = M;
            alpha = color.A;
        }

        /// <summary>Converts Towel.Graphics.Color values to HSV color values.</summary>
        /// <param name="hue">The HSV hue component.</param>
        /// <param name="saturation">The HSV saturation component.</param>
        /// <param name="value">The HSV value component.</param>
        /// <param name="alpha">The HSV alpha component.</param>
        public void ToHsv(out float hue, out float saturation, out float value, out float alpha)
        {
            Color.ToHsv(this, out hue, out saturation, out value, out alpha);
        }

        #endregion

        #region XYZ

        /// <summary>Converts XYZ color values to Towel.Graphics.Color values.</summary>
        /// <param name="x">The XYZ x component.</param>
        /// <param name="y">The XYZ y component.</param>
        /// <param name="z">The XYZ z component.</param>
        /// <param name="alpha">The XYZ alpha component.</param>
        /// <returns>The converted color value.</returns>
        /// <citation>
        /// This conversion algorithm was originally developed as part of the
        /// OpenTK open source project. Hoever, it has been modified since its
        /// addition to the Theta Framework.
        /// </citation>
        public static Color FromXyz(float x, float y, float z, float alpha)
        {
            float r = 0.41847f * x + -0.15866f * y + -0.082835f * z;
            float g = -0.091169f * x + 0.25243f * y + 0.015708f * z;
            float b = 0.00092090f * x + -0.0025498f * y + 0.17860f * z;
            return new Color(r, g, b, alpha);
        }

        /// <summary>Converts Towel.Graphics.Color values to XYZ color values.</summary>
        /// <param name="color">The Towel.Graphics.Color value to convert.</param>
        /// <param name="x">The XYZ x component.</param>
        /// <param name="y">The XYZ y component.</param>
        /// <param name="z">The XYZ z component.</param>
        /// <param name="alpha">The XYZ alpha component.</param>
        /// <citation>
        /// This conversion algorithm was originally developed as part of the
        /// OpenTK open source project. Hoever, it has been modified since its
        /// addition to the Theta Framework.
        /// </citation>
        public static void ToXyz(Color color, out float x, out float y, out float z, out float alpha)
        {
            x = (0.49f * color.R + 0.31f * color.G + 0.20f * color.B) / 0.17697f;
            y = (0.17697f * color.R + 0.81240f * color.G + 0.01063f * color.B) / 0.17697f;
            z = (0.00f * color.R + 0.01f * color.G + 0.99f * color.B) / 0.17697f;
            alpha =  color.A;
        }

        /// <summary>Converts Towel.Graphics.Color values to XYZ color values.</summary>
        /// <param name="x">The XYZ x component.</param>
        /// <param name="y">The XYZ y component.</param>
        /// <param name="z">The XYZ z component.</param>
        /// <param name="alpha">The XYZ alpha component.</param>
        public static void ToXyz(out float x, out float y, out float z, out float alpha)
        {
            Color.ToXyz(Thistle, out x, out y, out z, out alpha);
        }

        #endregion

        #region YCbCr aka YUV

        /// <summary>Converts YCbCr color values to Towel.Graphics.Color values.</summary>
        /// <param name="luma">The YCbCr luma component (0.0 to 1.0).</param>
        /// <param name="blueDifferenceChroma">The YCbCr blue-difference chroma component (-0.5 to 0.5).</param>
        /// <param name="redDifferenceChroma">The YCbCr red-difference chroma component (-0.5 to 0.5).</param>
        /// <param name="alpha">The YCbCr alpha component.</param>
        /// <returns>The converted color value.</returns>
        /// <citation>
        /// This conversion algorithm was originally developed as part of the
        /// OpenTK open source project. Hoever, it has been modified since its
        /// addition to the Theta Framework.
        /// </citation>
        public static Color FromYcbcr(float luma, float blueDifferenceChroma, float redDifferenceChroma, float alpha)
        {
            if (luma < 0f || luma > 1f)
            {
                throw new ArgumentOutOfRangeException(nameof(luma), luma, "!(0 <= " + nameof(luma) + " <= 1)");
            }
            if (blueDifferenceChroma < -0.5 || blueDifferenceChroma > 0.5f)
            {
                throw new ArgumentOutOfRangeException(nameof(blueDifferenceChroma), blueDifferenceChroma, "!(-0.5 <= " + nameof(blueDifferenceChroma) + " <= 0.5f)");
            }
            if (redDifferenceChroma < -0.5 || redDifferenceChroma > 0.5f)
            {
                throw new ArgumentOutOfRangeException(nameof(redDifferenceChroma), redDifferenceChroma, "!(-0.5 <= " + nameof(redDifferenceChroma) + " <= 0.5f)");
            }
            if (alpha < 0f || alpha > 1f)
            {
                throw new ArgumentOutOfRangeException(nameof(alpha), alpha, "!(0 <= " + nameof(alpha) + " <= 1)");
            }

            float r = 1.0f * luma + 0.0f * blueDifferenceChroma + 1.402f * redDifferenceChroma;
            float g = 1.0f * luma + -0.344136f * blueDifferenceChroma + -0.714136f * redDifferenceChroma;
            float b = 1.0f * luma + 1.772f * blueDifferenceChroma + 0.0f * redDifferenceChroma;
            return new Color(r, g, b, alpha);
        }

        /// <summary>Converts Towel.Graphics.Color values to a YCbCr color values.</summary>
        /// <param name="color">The Towel.Graphics.Color value to be converted.</param>
        /// <param name="luma">The YCbCr luma component (0.0 to 1.0).</param>
        /// <param name="blueDifferenceChroma">The YCbCr blue-difference chroma component (-0.5 to 0.5).</param>
        /// <param name="redDifferenceChroma">The YCbCr red-difference chroma component (-0.5 to 0.5).</param>
        /// <param name="alpha">The YCbCr alpha component.</param>
        /// <citation>
        /// This conversion algorithm was originally developed as part of the
        /// OpenTK open source project. Hoever, it has been modified since its
        /// addition to the Theta Framework.
        /// </citation>
        public static void ToYcbcr(Color color, out float luma, out float blueDifferenceChroma, out float redDifferenceChroma, out float alpha)
        {
            luma = 0.299f * color.R + 0.587f * color.G + 0.114f * color.B;
            blueDifferenceChroma = -0.168736f * color.R + -0.331264f * color.G + 0.5f * color.B;
            redDifferenceChroma = 0.5f * color.R + -0.418688f * color.G + -0.081312f * color.B;
            alpha = color.A;
        }

        /// <summary>Converts Towel.Graphics.Color values to a YCbCr color values.</summary>
        /// <param name="luma">The YCbCr luma component (0.0 to 1.0).</param>
        /// <param name="blueDifferenceChroma">The YCbCr blue-difference chroma component (-0.5 to 0.5).</param>
        /// <param name="redDifferenceChroma">The YCbCr red-difference chroma component (-0.5 to 0.5).</param>
        /// <param name="alpha">The YCbCr alpha component.</param>
        public void ToYcbcr(out float luma, out float blueDifferenceChroma, out float redDifferenceChroma, out float alpha)
        {
            Color.ToYcbcr(Thistle, out luma, out blueDifferenceChroma, out redDifferenceChroma, out alpha);
        }

        #endregion

        #region HCY

        /// <summary>Converts HCY color values to Towel.Graphics.Color values.</summary>
        /// <param name="hue">The HCY hue component.</param>
        /// <param name="chroma">The HCY chroma component.</param>
        /// <param name="luminance">The HCY luminance component.</param>
        /// <param name="alpha">The HCY alpha compnent.</param>
        /// <returns>The converted color value.</returns>
        /// <citation>
        /// This conversion algorithm was originally developed as part of the
        /// OpenTK open source project. Hoever, it has been modified since its
        /// addition to the Theta Framework.
        /// </citation>
        public static Color FromHcy(float hue, float chroma, float luminance, float alpha)
        {
            if (hue < 0f || hue > 1f)
            {
                throw new ArgumentOutOfRangeException(nameof(hue), hue, "!(0 <= " + nameof(hue) + " <= 1)");
            }
            if (chroma < 0f || chroma > 1f)
            {
                throw new ArgumentOutOfRangeException(nameof(chroma), chroma, "!(0 <= " + nameof(chroma) + " <= 1)");
            }
            if (luminance < 0f || luminance > 1f)
            {
                throw new ArgumentOutOfRangeException(nameof(luminance), luminance, "!(0 <= " + nameof(luminance) + " <= 1)");
            }
            if (alpha < 0f || alpha > 1f)
            {
                throw new ArgumentOutOfRangeException(nameof(alpha), alpha, "!(0 <= " + nameof(alpha) + " <= 1)");
            }

            hue = hue * 360.0f;

            var h = hue / 60.0f;
            var X = chroma * (1.0f - Math.Abs(h % 2.0f - 1.0f));

            float r, g, b;
            if (0.0f <= h && h < 1.0f)
            {
                r = chroma;
                g = X;
                b = 0.0f;
            }
            else if (1.0f <= h && h < 2.0f)
            {
                r = X;
                g = chroma;
                b = 0.0f;
            }
            else if (2.0f <= h && h < 3.0f)
            {
                r = 0.0f;
                g = chroma;
                b = X;
            }
            else if (3.0f <= h && h < 4.0f)
            {
                r = 0.0f;
                g = X;
                b = chroma;
            }
            else if (4.0f <= h && h < 5.0f)
            {
                r = X;
                g = 0.0f;
                b = chroma;
            }
            else if (5.0f <= h && h < 6.0f)
            {
                r = chroma;
                g = 0.0f;
                b = X;
            }
            else
            {
                r = 0.0f;
                g = 0.0f;
                b = 0.0f;
            }

            var m = luminance - (0.30f * r + 0.59f * g + 0.11f * b);

            return new Color(r + m, g + m, b + m, alpha);
        }

        /// <summary>Converts Towel.Graphics.Color values to HCY color values.</summary>
        /// <param name="color">The Towel.Graphics.Color value to convert.</param>
        /// <param name="hue">The HCY hue component.</param>
        /// <param name="chroma">The HCY chroma component.</param>
        /// <param name="luminance">The HCY luminance component.</param>
        /// <param name="alpha">The HCY alpha compnent.</param>
        /// <citation>
        /// This conversion algorithm was originally developed as part of the
        /// OpenTK open source project. Hoever, it has been modified since its
        /// addition to the Theta Framework.
        /// </citation>
        public static void ToHcy(Color color, out float hue, out float chroma, out float luminance, out float alpha)
        {
            var M = Math.Max(color.R, Math.Max(color.G, color.B));
            var m = Math.Min(color.R, Math.Min(color.G, color.B));
            chroma = M - m;

            float h = 0.0f;
            if (M == color.R)
            {
                h = ((color.G - color.B) / chroma) % 6.0f;
            }
            else if (M == color.G)
            {
                h = ((color.B - color.R) / chroma) + 2.0f;
            }
            else if (M == color.B)
            {
                h = ((color.R - color.G) / chroma) + 4.0f;
            }

            hue = (h * 60.0f) / 360.0f;
            luminance = 0.30f * color.R + 0.59f * color.G + 0.11f * color.B;
            alpha = color.A;
        }

        /// <summary>Converts Towel.Graphics.Color values to HCY color values.</summary>
        /// <param name="hue">The HCY hue component.</param>
        /// <param name="chroma">The HCY chroma component.</param>
        /// <param name="luminance">The HCY luminance component.</param>
        /// <param name="alpha">The HCY alpha compnent.</param>
        public void ToHcy(out float hue, out float chroma, out float luminance, out float alpha)
        {
            Color.ToHcy(this, out hue, out chroma, out luminance, out alpha);
        }

        #endregion

        #region RGBA-255

        /// <summary>Converts RGBA-255 color values to Towel.Graphics.Color values.</summary>
        /// <param name="red">The RGBA-255 red component.</param>
        /// <param name="green">The RGBA-255 green component.</param>
        /// <param name="blue">The RGBA-255 blue component.</param>
        /// <param name="alpha">The RGBA-255 alpha component.</param>
        /// <returns>The converted color value.</returns>
        public static Color FromRgba255(int red, int green, int blue, int alpha)
		{
            if (0 > red || red > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(red), red, "!(0 <= " + nameof(red) + " <= 255)");
            }
            if (0 > green || green > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(green), green, "!(0 <= " + nameof(green) + " <= 255)");
            }
            if (0 > blue || blue > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(blue), blue, "!(0 <= " + nameof(blue) + " <= 255)");
            }
            if (0 > alpha || alpha > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(alpha), alpha, "!(0 <= " + nameof(alpha) + " <= 255)");
            }

			return new Color((float)red / 255F, (float)green / 255F, (float)blue / 255F, (float)alpha / 255F);
		}

        /// <summary>Converts Towel.Graphics.Color values to RGBA-255 color values.</summary>
        /// <param name="color">The Towel.Graphics.Color value to convert.</param>
        /// <param name="red">The RGBA-255 red component.</param>
        /// <param name="green">The RGBA-255 green component.</param>
        /// <param name="blue">The RGBA-255 blue component.</param>
        /// <param name="alpha">The RGBA-255 alpha component.</param>
        public static void ToRgba255(Color color, out int red, out int green, out int blue, out int alpha)
        {
            red   = (int)(color.R * 255f);
            green = (int)(color.G * 255f);
            blue  = (int)(color.B * 255f);
            alpha = (int)(color.A * 255f);
        }

        /// <summary>Converts Towel.Graphics.Color values to RGBA-255 color values.</summary>
        /// <param name="red">The RGBA-255 red component.</param>
        /// <param name="green">The RGBA-255 green component.</param>
        /// <param name="blue">The RGBA-255 blue component.</param>
        /// <param name="alpha">The RGBA-255 alpha component.</param>
        public void ToRgba255(out int red, out int green, out int blue, out int alpha)
        {
            Color.ToRgba255(this, out red, out green, out blue, out alpha);
        }

        #endregion

        #endregion

        #region Color Library
        /// <summary>Hex: 0x000000.</summary>
		public static readonly Color Black = FromRgba255(0, 0, 0, 0);
		/// <summary>Hex: 0xffffff.</summary>
		public static readonly Color White = FromRgba255(255, 255, 255, 0);

		/// <summary>Hex: 0x0000ff.</summary>
		public static readonly Color Blue = FromRgba255(0, 0, 255, 0);
		/// <summary>Hex: 0x00ff00.</summary>
		public static readonly Color Green = FromRgba255(0, 255, 0, 0);
		/// <summary>Hex: 0xff0000.</summary>
		public static readonly Color Red = FromRgba255(255, 0, 0, 0);

		/// <summary>Hex: 0x00ffff.</summary>
		public static readonly Color Cyan = FromRgba255(0, 255, 255, 0);
		/// <summary>Hex: 0xff00ff.</summary>
		public static readonly Color Magenta = FromRgba255(255, 0, 255, 0);
		/// <summary>Hex: 0xffff00.</summary>
		public static readonly Color Yellow = FromRgba255(255, 255, 0, 0);

		/// <summary>Hex: 0xf0f8ff.</summary>
		public static readonly Color AliceBlue = FromRgba255(240, 248, 255, 0);
		/// <summary>Hex: 0xfaebd7.</summary>
		public static readonly Color AntiqueWhite = FromRgba255(250, 235, 215, 0);
		/// <summary>Hex: 0x00ffff.</summary>
		public static readonly Color Aqua = FromRgba255(0, 255, 255, 0);
		/// <summary>Hex: 0x7fffd4.</summary>
		public static readonly Color Aquamarine = FromRgba255(127, 255, 212, 0);
		/// <summary>Hex: 0xf0ffff.</summary>
		public static readonly Color Azure = FromRgba255(240, 255, 255, 0);
		/// <summary>Hex: 0xf5f5dc.</summary>
		public static readonly Color Beige = FromRgba255(245, 245, 220, 0);
		/// <summary>Hex: 0xffe4c4.</summary>
		public static readonly Color Bisque = FromRgba255(255, 228, 196, 0);
		/// <summary>Hex: 0xffebcd.</summary>
		public static readonly Color BlanchedAlmond = FromRgba255(255, 235, 205, 0);
		/// <summary>Hex: 0x8a2be2.</summary>
		public static readonly Color BlueViolet = FromRgba255(138, 43, 226, 0);
		/// <summary>Hex: 0xa52a2a.</summary>
		public static readonly Color Brown = FromRgba255(165, 42, 42, 0);
		/// <summary>Hex: 0xdeb887.</summary>
		public static readonly Color Burlywood = FromRgba255(222, 184, 135, 0);
		/// <summary>Hex: 0x5f9ea0.</summary>
		public static readonly Color CadetBlue = FromRgba255(95, 158, 160, 0);
		/// <summary>Hex: 0x7fff00.</summary>
		public static readonly Color Chartreuse = FromRgba255(127, 255, 0, 0);
		/// <summary>Hex: 0xd2691e.</summary>
		public static readonly Color Chocolate = FromRgba255(210, 105, 30, 0);
		/// <summary>Hex: 0xff7f50.</summary>
		public static readonly Color Coral = FromRgba255(255, 127, 80, 0);
		/// <summary>Hex: 0x6495ed.</summary>
		public static readonly Color CornflowerBlue = FromRgba255(100, 149, 237, 0);
		/// <summary>Hex: 0xfff8dc.</summary>
		public static readonly Color Cornsilk = FromRgba255(255, 248, 220, 0);
		/// <summary>Hex: 0x00008b.</summary>
		public static readonly Color DarkBlue = FromRgba255(0, 0, 139, 0);
		/// <summary>Hex: 0x008b8b.</summary>
		public static readonly Color DarkCyan = FromRgba255(0, 139, 139, 0);
		/// <summary>Hex: 0xb8860b.</summary>
		public static readonly Color DarkGoldenrod = FromRgba255(184, 134, 11, 0);
		/// <summary>Hex: 0xa9a9a9.</summary>
		public static readonly Color DarkGray = FromRgba255(169, 169, 169, 0);
		/// <summary>Hex: 0x006400.</summary>
		public static readonly Color DarkGreen = FromRgba255(0, 100, 0, 0);
		/// <summary>Hex: 0xbdb76b.</summary>
		public static readonly Color DarkKhaki = FromRgba255(189, 183, 107, 0);
		/// <summary>Hex: 0x8b008b.</summary>
		public static readonly Color DarkMagenta = FromRgba255(139, 0, 139, 0);
		/// <summary>Hex: 0x556b2f.</summary>
		public static readonly Color DarkOliveGreen = FromRgba255(85, 107, 47, 0);
		/// <summary>Hex: 0xff8c00.</summary>
		public static readonly Color DarkOrange = FromRgba255(255, 140, 0, 0);
		/// <summary>Hex: 0x9932cc.</summary>
		public static readonly Color DarkOrchid = FromRgba255(153, 50, 204, 0);
		/// <summary>Hex: 0x8b0000.</summary>
		public static readonly Color DarkRed = FromRgba255(139, 0, 0, 0);
		/// <summary>Hex: 0xe9967a.</summary>
		public static readonly Color DarkSalmon = FromRgba255(233, 150, 122, 0);
		/// <summary>Hex: 0x8fbc8f.</summary>
		public static readonly Color DarkSeaGreen = FromRgba255(143, 188, 143, 0);
		/// <summary>Hex: 0x483d8b.</summary>
		public static readonly Color DarkSlateBlue = FromRgba255(72, 61, 139, 0);
		/// <summary>Hex: 0x2f4f4f.</summary>
		public static readonly Color DarkSlateGray = FromRgba255(47, 79, 79, 0);
		/// <summary>Hex: 0x00ced1.</summary>
		public static readonly Color DarkTurquoise = FromRgba255(0, 206, 209, 0);
		/// <summary>Hex: 0x9400d3.</summary>
		public static readonly Color DarkViolet = FromRgba255(148, 0, 211, 0);
		/// <summary>Hex: 0xff1493.</summary>
		public static readonly Color DeepPink = FromRgba255(255, 20, 147, 0);
		/// <summary>Hex: 0x00bfff.</summary>
		public static readonly Color DeepSkyBlue = FromRgba255(0, 191, 255, 0);
		/// <summary>Hex: 0x696969.</summary>
		public static readonly Color DimGray = FromRgba255(105, 105, 105, 0);
		/// <summary>Hex: 0x1e90ff.</summary>
		public static readonly Color DodgerBlue = FromRgba255(30, 144, 255, 0);
		/// <summary>Hex: 0xb22222.</summary>
		public static readonly Color Firebrick = FromRgba255(178, 34, 34, 0);
		/// <summary>Hex: 0xfffaf0.</summary>
		public static readonly Color FloralWhite = FromRgba255(255, 250, 240, 0);
		/// <summary>Hex: 0x228b22.</summary>
		public static readonly Color ForestGreen = FromRgba255(34, 139, 34, 0);
		/// <summary>Hex: 0xff00ff.</summary>
		public static readonly Color Fuschia = FromRgba255(255, 0, 255, 0);
		/// <summary>Hex: 0xdcdcdc.</summary>
		public static readonly Color Gainsboro = FromRgba255(220, 220, 220, 0);
		/// <summary>Hex: 0xf8f8ff.</summary>
		public static readonly Color GhostWhite = FromRgba255(255, 250, 250, 0);
		/// <summary>Hex: 0xffd700.</summary>
		public static readonly Color Gold = FromRgba255(255, 215, 0, 0);
		/// <summary>Hex: 0xdaa520.</summary>
		public static readonly Color Goldenrod = FromRgba255(218, 165, 32, 0);
		/// <summary>Hex: 0x808080.</summary>
		public static readonly Color Gray = FromRgba255(128, 128, 128, 0);
		/// <summary>Hex: 0xadff2f.</summary>
		public static readonly Color GreenYellow = FromRgba255(173, 255, 47, 0);
		/// <summary>Hex: 0xf0fff0.</summary>
		public static readonly Color Honeydew = FromRgba255(240, 255, 240, 0);
		/// <summary>Hex: 0xff69b4.</summary>
		public static readonly Color HotPink = FromRgba255(255, 105, 180, 0);
		/// <summary>Hex: 0xcd5c5c.</summary>
		public static readonly Color IndianRed = FromRgba255(205, 92, 92, 0);
		/// <summary>Hex: 0xfffff0.</summary>
		public static readonly Color Ivory = FromRgba255(255, 255, 240, 0);
		/// <summary>Hex: 0xf0e68c.</summary>
		public static readonly Color Khaki = FromRgba255(240, 230, 140, 0);
		/// <summary>Hex: 0xe6e6fa.</summary>
		public static readonly Color Lavender = FromRgba255(230, 230, 250, 0);
		/// <summary>Hex: 0xfff0f5.</summary>
		public static readonly Color LavenderBlush = FromRgba255(255, 240, 245, 0);
		/// <summary>Hex: 0x7cfc00.</summary>
		public static readonly Color LawnGreen = FromRgba255(124, 252, 0, 0);
		/// <summary>Hex: 0xfffacd.</summary>
		public static readonly Color LemonChiffon = FromRgba255(255, 250, 205, 0);
		/// <summary>Hex: 0xadd8e6.</summary>
		public static readonly Color LightBlue = FromRgba255(173, 216, 230, 0);
		/// <summary>Hex: 0xf08080.</summary>
		public static readonly Color LightCoral = FromRgba255(240, 138, 138, 0);
		/// <summary>Hex: 0xe0ffff.</summary>
		public static readonly Color LightCyan = FromRgba255(224, 255, 255, 0);
		/// <summary>Hex: 0xeedd82.</summary>
		public static readonly Color LightGoldenrod = FromRgba255(238, 221, 130, 0);
		/// <summary>Hex: 0xfafad2.</summary>
		public static readonly Color LightGoldenrodYellow = FromRgba255(250, 250, 210, 0);
		/// <summary>Hex: 0xd3d3d3.</summary>
		public static readonly Color LightGray = FromRgba255(211, 211, 211, 0);
		/// <summary>Hex: 0x90ee90.</summary>
		public static readonly Color LightGreen = FromRgba255(144, 238, 144, 0);
		/// <summary>Hex: 0xffb6c1.</summary>
		public static readonly Color LightPink = FromRgba255(255, 182, 193, 0);
		/// <summary>Hex: 0xffa07a.</summary>
		public static readonly Color LightSalmon = FromRgba255(255, 160, 122, 0);
		/// <summary>Hex: 0x20b2aa.</summary>
		public static readonly Color LightSeaGreen = FromRgba255(32, 178, 170, 0);
		/// <summary>Hex: 0x87cefa.</summary>
		public static readonly Color LightSkyBlue = FromRgba255(135, 206, 250, 0);
		/// <summary>Hex: 0x8470ff.</summary>
		public static readonly Color LightSlateBlue = FromRgba255(132, 112, 255, 0);
		/// <summary>Hex: 0x778899.</summary>
		public static readonly Color LightSlateGray = FromRgba255(119, 136, 153, 0);
		/// <summary>Hex: 0xb0c4de.</summary>
		public static readonly Color LightSteelBlue = FromRgba255(176, 196, 222, 0);
		/// <summary>Hex: 0xffffe0.</summary>
		public static readonly Color LightYellow = FromRgba255(255, 255, 224, 0);
		/// <summary>Hex: 0x00ff00.</summary>
		public static readonly Color Lime = FromRgba255(0, 255, 0, 0);
		/// <summary>Hex: 0x32cd32.</summary>
		public static readonly Color LimeGreen = FromRgba255(50, 205, 50, 0);
		/// <summary>Hex: 0xfaf0e6.</summary>
		public static readonly Color Linen = FromRgba255(250, 240, 230, 0);
		/// <summary>Hex: 0x800000.</summary>
		public static readonly Color Maroon = FromRgba255(128, 0, 0, 0);
		/// <summary>Hex: 0x66cdaa.</summary>
		public static readonly Color MediumAquamarine = FromRgba255(102, 205, 170, 0);
		/// <summary>Hex: 0x0000cd.</summary>
		public static readonly Color MediumBlue = FromRgba255(0, 0, 205, 0);
		/// <summary>Hex: 0xba55d3.</summary>
		public static readonly Color MediumOrchid = FromRgba255(186, 85, 211, 0);
		/// <summary>Hex: 0x9370db.</summary>
		public static readonly Color MediumPurple = FromRgba255(147, 112, 219, 0);
		/// <summary>Hex: 0x3cb371.</summary>
		public static readonly Color MediumSeaGreen = FromRgba255(60, 179, 113, 0);
		/// <summary>Hex: 0x7b68ee.</summary>
		public static readonly Color MediumSlateBlue = FromRgba255(123, 104, 238, 0);
		/// <summary>Hex: 0x00fa9a.</summary>
		public static readonly Color MediumSpringGreen = FromRgba255(0, 250, 154, 0);
		/// <summary>Hex: 0x48d1cc.</summary>
		public static readonly Color MediumTurquoise = FromRgba255(72, 209, 204, 0);
		/// <summary>Hex: 0xc71585.</summary>
		public static readonly Color MediumVioletRed = FromRgba255(199, 21, 133, 0);
		/// <summary>Hex: 0x191970.</summary>
		public static readonly Color MidnightBlue = FromRgba255(25, 25, 112, 0);
		/// <summary>Hex: 0xf5fffa.</summary>
		public static readonly Color MintCream = FromRgba255(245, 255, 250, 0);
		/// <summary>Hex: 0xe1e4e1.</summary>
		public static readonly Color MistyRose = FromRgba255(255, 228, 225, 0);
		/// <summary>Hex: 0xffe4b5.</summary>
		public static readonly Color Moccasin = FromRgba255(255, 228, 181, 0);
		/// <summary>Hex: 0xffdead.</summary>
		public static readonly Color NavajoWhite = FromRgba255(255, 222, 173, 0);
		/// <summary>Hex: 0x000080.</summary>
		public static readonly Color Navy = FromRgba255(0, 0, 128, 0);
		/// <summary>Hex: 0xfdf5e6.</summary>
		public static readonly Color OldLace = FromRgba255(253, 245, 230, 0);
		/// <summary>Hex: 0x808000.</summary>
		public static readonly Color Olive = FromRgba255(128, 128, 0, 0);
		/// <summary>Hex: 0x6b8e23.</summary>
		public static readonly Color OliveDrab = FromRgba255(107, 142, 35, 0);
		/// <summary>Hex: 0xffa500.</summary>
		public static readonly Color Orange = FromRgba255(255, 165, 0, 0);
		/// <summary>Hex: 0xff4500.</summary>
		public static readonly Color OrangeRed = FromRgba255(255, 69, 0, 0);
		/// <summary>Hex: 0xda70d6.</summary>
		public static readonly Color Orchid = FromRgba255(218, 112, 214, 0);
		/// <summary>Hex: 0xeee8aa.</summary>
		public static readonly Color PaleGoldenrod = FromRgba255(238, 232, 170, 0);
		/// <summary>Hex: 0x98fb98.</summary>
		public static readonly Color PaleGreen = FromRgba255(152, 251, 152, 0);
		/// <summary>Hex: 0xafeeee.</summary>
		public static readonly Color PaleTurquoise = FromRgba255(175, 238, 238, 0);
		/// <summary>Hex: 0xdb7093.</summary>
		public static readonly Color PaleVioletRed = FromRgba255(219, 112, 147, 0);
		/// <summary>Hex: 0xffefd5.</summary>
		public static readonly Color PapayaWhip = FromRgba255(255, 239, 213, 0);
		/// <summary>Hex: 0xffdab9.</summary>
		public static readonly Color PeachPuff = FromRgba255(255, 218, 185, 0);
		/// <summary>Hex: 0xcd853f.</summary>
		public static readonly Color Peru = FromRgba255(205, 133, 63, 0);
		/// <summary>Hex: 0xffc0cb.</summary>
		public static readonly Color Pink = FromRgba255(255, 192, 203, 0);
		/// <summary>Hex: 0xdda0dd.</summary>
		public static readonly Color Plum = FromRgba255(221, 160, 221, 0);
		/// <summary>Hex: 0xb0e0e6.</summary>
		public static readonly Color PowderBlue = FromRgba255(176, 224, 230, 0);
		/// <summary>Hex: 0x800080.</summary>
		public static readonly Color Purple = FromRgba255(128, 0, 128, 0);
		/// <summary>Hex: 0xbc8f8f.</summary>
		public static readonly Color RosyBrown = FromRgba255(188, 143, 143, 0);
		/// <summary>Hex: 0x4169e1.</summary>
		public static readonly Color RoyalBlue = FromRgba255(65, 105, 225, 0);
		/// <summary>Hex: 0x8b4513.</summary>
		public static readonly Color SaddleBrown = FromRgba255(139, 69, 19, 0);
		/// <summary>Hex: 0xfa8072.</summary>
		public static readonly Color Salmon = FromRgba255(250, 128, 114, 0);
		/// <summary>Hex: 0xf4a460.</summary>
		public static readonly Color SandyBrown = FromRgba255(244, 164, 96, 0);
		/// <summary>Hex: 0x2e8b57.</summary>
		public static readonly Color SeaGreen = FromRgba255(46, 139, 87, 0);
		/// <summary>Hex: 0xfff5ee.</summary>
		public static readonly Color Seashell = FromRgba255(255, 245, 238, 0);
		/// <summary>Hex: 0xa0522d.</summary>
		public static readonly Color Sienna = FromRgba255(160, 82, 45, 0);
		/// <summary>Hex: 0xc0c0c0.</summary>
		public static readonly Color Silver = FromRgba255(192, 192, 192, 0);
		/// <summary>Hex: 0x87ceeb.</summary>
		public static readonly Color SkyBlue = FromRgba255(135, 206, 235, 0);
		/// <summary>Hex: 0x6a5acd.</summary>
		public static readonly Color SlateBlue = FromRgba255(106, 90, 205, 0);
		/// <summary>Hex: 0x708090.</summary>
		public static readonly Color SlateGray = FromRgba255(112, 128, 144, 0);
		/// <summary>Hex: 0xfffafa.</summary>
		public static readonly Color Snow = FromRgba255(255, 250, 250, 0);
		/// <summary>Hex: 0x00ff7f.</summary>
		public static readonly Color SpringGreen = FromRgba255(0, 255, 127, 0);
		/// <summary>Hex: 0x4682b4.</summary>
		public static readonly Color SteelBlue = FromRgba255(70, 130, 180, 0);
		/// <summary>Hex: 0xd2b48c.</summary>
		public static readonly Color Tan = FromRgba255(210, 180, 140, 0);
		/// <summary>Hex: 0x008080.</summary>
		public static readonly Color Teal = FromRgba255(0, 128, 128, 0);
		/// <summary>Hex: 0xd8bfd8.</summary>
		public static readonly Color Thistle = FromRgba255(216, 191, 216, 0);
		/// <summary>Hex: 0xff6347.</summary>
		public static readonly Color Tomato = FromRgba255(255, 99, 71, 0);
		/// <summary>Hex: 0x40e0d0.</summary>
		public static readonly Color Turquoise = FromRgba255(64, 224, 208, 0);
		/// <summary>Hex: 0xee82ee.</summary>
		public static readonly Color Violet = FromRgba255(238, 130, 238, 0);
		/// <summary>Hex: 0xd02090.</summary>
		public static readonly Color VioletRed = FromRgba255(208, 32, 144, 0);
		/// <summary>Hex: 0xf5deb3.</summary>
		public static readonly Color Wheat = FromRgba255(245, 222, 179, 0);
		/// <summary>Hex: 0xf5f5f5.</summary>
		public static readonly Color WhiteSmoke = FromRgba255(245, 245, 245, 0);
		/// <summary>Hex: 0x9acd32.</summary>
		public static readonly Color YellowGreen = FromRgba255(154, 205, 50, 0);
		#endregion

        #region Overrides

        public override int GetHashCode()
        {
            return
                this.R.GetHashCode() ^
                this.G.GetHashCode() ^
                this.B.GetHashCode() ^
                this.A.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Color)
            {
                return Color.Equals(this, (Color)obj);
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
