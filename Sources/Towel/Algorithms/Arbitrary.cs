namespace Towel.Algorithms
{
    /// <summary>
    /// 
    /// </summary>
    /// <citation>
    /// This code has been adapted from the Math.Net Numerics project. Here is their license and plugs:
    /// 
    /// Math.NET Numerics, part of the Math.NET Project
    /// http://numerics.mathdotnet.com
    /// http://github.com/mathnet/mathnet-numerics
    /// http://mathnetnumerics.codeplex.com
    ///
    /// Copyright (c) 2009-2014 Math.NET
    ///
    /// Permission is hereby granted, free of charge, to any person
    /// obtaining a copy of this software and associated documentation
    /// files (the "Software"), to deal in the Software without
    /// restriction, including without limitation the rights to use,
    /// copy, modify, merge, publish, distribute, sublicense, and/or sell
    /// copies of the Software, and to permit persons to whom the
    /// Software is furnished to do so, subject to the following
    /// conditions:
    ///
    /// The above copyright notice and this permission notice shall be
    /// included in all copies or substantial portions of the Software.
    ///
    /// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
    /// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
    /// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
    /// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
    /// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
    /// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
    /// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
    /// OTHER DEALINGS IN THE SOFTWARE.
    /// </citation>
    public class Arbitrary : System.Random
    {
        #region Fields

        AlgorithmWrapper _wrapper;

        #endregion

        #region Internal Wrappers

        internal interface AlgorithmWrapper
        {
            int Next();
            int Next(int max);
            int Next(int min, int max);
            void NextBytes(byte[] buffer);
            double NextDouble();
        }

        internal class IntegerWrapper : AlgorithmWrapper
        {
            Arbitrary.Algorithms.Integer _algorithm;

            public IntegerWrapper(Arbitrary.Algorithms.Integer algorithm)
            {
                this._algorithm = algorithm;
            }

            public int Next() { return this._algorithm.Next(); }
            public int Next(int max) { return this._algorithm.Next() % max; }
            public int Next(int min, int max) { return this._algorithm.Next() % (max - min) + min; }
            public void NextBytes(byte[] buffer) { throw new System.NotImplementedException(); }
            public double NextDouble() { return 1.0D / (double)this._algorithm.Next(); }
        }

        internal class DoubleWrapper : AlgorithmWrapper
        {
            Arbitrary.Algorithms.Double _algorithm;

            public DoubleWrapper(Arbitrary.Algorithms.Double algorithm)
            {
                this._algorithm = algorithm;
            }

            public int Next() { return (int)(int.MaxValue * this._algorithm.Next()); }
            public int Next(int max) { return (int)(this._algorithm.Next() * max); }
            public int Next(int min, int max) { return (int)(this._algorithm.Next() * (max - min) + min); }
            public void NextBytes(byte[] buffer) { throw new System.NotImplementedException(); }
            public double NextDouble() { return this._algorithm.Next(); }
        }

        #endregion

        #region Provided Algorithms

        public static class Algorithms
        {
            public abstract class Integer { public abstract int Next(); }
            public abstract class Double { public abstract double Next(); }

            #region MultiplicativeCongruent

            public class MultiplicativeCongruent : Arbitrary.Algorithms.Double
            {
                double _reciprocal;
                ulong _xn;
                ulong _modulus;
                ulong _multiplier;

                public MultiplicativeCongruent(int seed, ulong modulus, ulong multiplier)
                {
                    _multiplier = multiplier;
                    _modulus = modulus;
                    _reciprocal = 1.0d / _modulus;
                    _xn = (uint)seed % _modulus;
                }

                public override double Next()
                {
                    double ret = _xn * _reciprocal;
                    _xn = (_xn * _multiplier) % _modulus;
                    return ret;
                }
            }

            #endregion

            #region MultiplicativeCongruent_A

            public class MultiplicativeCongruent_A : MultiplicativeCongruent
            {
                // Modulus = 2 ^ 31 - 1
                // Multiplier = 1132489760

                public MultiplicativeCongruent_A() : this(System.Environment.TickCount) { }

                public MultiplicativeCongruent_A(int seed) : base(seed, 2147483647, 1132489760) { }
            }

            #endregion

            #region MultiplicativeCongruent_B

            public class MultiplicativeCongruent_B : MultiplicativeCongruent
            {
                // Modulus = 2 ^ 59
                // Multiplier = 13 ^ 13

                public MultiplicativeCongruent_B() : this(System.Environment.TickCount) { }

                public MultiplicativeCongruent_B(int seed) : base(seed, 576460752303423488, 302875106592253) { }
            }

            #endregion

            #region MersenneTwister

            public class MersenneTwister : Arbitrary.Algorithms.Integer
            {
                const uint LowerMask = 0x7fffffff;
                const int M = 397;
                const uint MatrixA = 0x9908b0df;
                const int N = 624;
                const uint UpperMask = 0x80000000;
                uint[] Mag01 = { 0x0U, MatrixA };
                uint[] _mt = new uint[N];
                int _mti = N + 1;

                public MersenneTwister() : this(System.Environment.TickCount) { }

                public MersenneTwister(int seed)
                {
                    _mt[0] = (uint)seed & 0xffffffff;
                    for (_mti = 1; _mti < N; _mti++)
                    {
                        _mt[_mti] = 1812433253 * (_mt[_mti - 1] ^ (_mt[_mti - 1] >> 30)) + (uint)_mti;
                        _mt[_mti] &= 0xffffffff;
                    }
                }

                public override int Next()
                {
                    int integer;
                    do
                    {
                        uint y;
                        if (_mti >= N)
                        {
                            int kk;
                            for (kk = 0; kk < N - M; kk++)
                            {
                                y = (_mt[kk] & UpperMask) | (_mt[kk + 1] & LowerMask);
                                _mt[kk] = _mt[kk + M] ^ (y >> 1) ^ Mag01[y & 0x1];
                            }
                            for (; kk < N - 1; kk++)
                            {
                                y = (_mt[kk] & UpperMask) | (_mt[kk + 1] & LowerMask);
                                _mt[kk] = _mt[kk + (M - N)] ^ (y >> 1) ^ Mag01[y & 0x1];
                            }
                            y = (_mt[N - 1] & UpperMask) | (_mt[0] & LowerMask);
                            _mt[N - 1] = _mt[M - 1] ^ (y >> 1) ^ Mag01[y & 0x1];

                            _mti = 0;
                        }
                        y = _mt[_mti++];
                        y ^= y >> 11;
                        y ^= (y << 7) & 0x9d2c5680;
                        y ^= (y << 15) & 0xefc60000;
                        y ^= y >> 18;
                        integer = (int)(y >> 1);
                    } while (integer == int.MaxValue);
                    return integer;
                }
            }

            #endregion

            #region CombinedMultipleRecursive

            public class CombinedMultipleRecursive : Arbitrary.Algorithms.Double
            {
                // 32bit 2 Components 3 Orders

                const double A12 = 1403580;
                const double A13 = 810728;
                const double A21 = 527612;
                const double A23 = 1370589;
                const double Modulus1 = 4294967087;
                const double Modulus2 = 4294944443;
                const double Reciprocal = 1.0 / Modulus1;
                double _xn1 = 1;
                double _xn2 = 1;
                double _yn1 = 1;
                double _yn2 = 1;
                double _yn3 = 1;

                double _xn3;

                public CombinedMultipleRecursive() : this(System.Environment.TickCount) { }

                public CombinedMultipleRecursive(int seed)
                {
                    _xn3 = (uint)seed;
                }

                public override double Next()
                {
                    double xn = A12 * _xn2 - A13 * _xn3;
                    double k = (long)(xn / Modulus1);
                    xn -= k * Modulus1;
                    if (xn < 0)
                        xn += Modulus1;
                    double yn = A21 * _yn1 - A23 * _yn3;
                    k = (long)(yn / Modulus2);
                    yn -= k * Modulus2;
                    if (yn < 0)
                        yn += Modulus2;
                    _xn3 = _xn2;
                    _xn2 = _xn1;
                    _xn1 = xn;
                    _yn3 = _yn2;
                    _yn2 = _yn1;
                    _yn1 = yn;
                    if (xn <= yn)
                        return (xn - yn + Modulus1) * Reciprocal;
                    return (xn - yn) * Reciprocal;
                }
            }

            #endregion

            #region WichmannHills1982

            public class WichmannHills1982 : Arbitrary.Algorithms.Double
            {
                // CombinedMultiplicativeCongruential

                const uint Modx = 30269;
                const double ModxRecip = 1.0 / Modx;
                const uint Mody = 30307;
                const double ModyRecip = 1.0 / Mody;
                const uint Modz = 30323;
                const double ModzRecip = 1.0 / Modz;
                uint _yn = 1;
                uint _zn = 1;

                uint _xn;

                public WichmannHills1982() : this(System.Environment.TickCount) { }

                public WichmannHills1982(int seed)
                {
                    _xn = (uint)seed % Modx;
                }

                public override double Next()
                {
                    _xn = (171 * _xn) % Modx;
                    _yn = (172 * _yn) % Mody;
                    _zn = (170 * _zn) % Modz;
                    double w = _xn * ModxRecip + _yn * ModyRecip + _zn * ModzRecip;
                    w -= (int)w;
                    return w;
                }
            }

            #endregion

            #region WichmannHills2006

            public class WichmannHills2006 : Arbitrary.Algorithms.Double
            {
                // CombinedMultiplicativeCongruential

                const uint Modw = 2147483123;
                const double ModwRecip = 1.0 / Modw;
                const uint Modx = 2147483579;
                const double ModxRecip = 1.0 / Modx;
                const uint Mody = 2147483543;
                const double ModyRecip = 1.0 / Mody;
                const uint Modz = 2147483423;
                const double ModzRecip = 1.0 / Modz;

                ulong _wn = 1;
                ulong _yn = 1;
                ulong _zn = 1;

                ulong _xn;

                public WichmannHills2006() : this(System.Environment.TickCount) { }

                public WichmannHills2006(int seed)
                {
                    _xn = (uint)seed % Modx;
                }

                public override double Next()
                {
                    _xn = 11600 * _xn % Modx;
                    _yn = 47003 * _yn % Mody;
                    _zn = 23000 * _zn % Modz;
                    _wn = 33000 * _wn % Modw;
                    double u = _xn * ModxRecip + _yn * ModyRecip + _zn * ModzRecip + _wn * ModwRecip;
                    u -= (int)u;
                    return u;
                }
            }

            #endregion

            #region MultiplyWithCarryXorshift

            public class MultiplyWithCarryXorshift : Arbitrary.Algorithms.Integer
            {
                ulong _x; // Seed or last but three unsigned random number
                ulong _y; // Last but two unsigned random number
                ulong _z; // Last but one unsigned random number
                ulong _c; // The value of the carry over
                ulong _a; // The multiplier

                public MultiplyWithCarryXorshift() : this(System.Environment.TickCount) { }

                public MultiplyWithCarryXorshift(int seed) : this(seed, 916905990, 13579, 362436069, 77465321) { }

                public MultiplyWithCarryXorshift(int seed, long a, long c, long x1, long x2)
                {
                    if (seed == 0)
                        seed = 1;
                    if (a <= c)
                        throw new System.ArgumentOutOfRangeException("c", "c must be greater than or equal to a");
                    _x = (uint)seed;
                    _y = (ulong)x1;
                    _z = (ulong)x2;
                    _a = (ulong)a;
                    _c = (ulong)c;
                }

                public override int Next()
                {
                    int int31;
                    do
                    {
                        var t = (_a * _x) + _c;
                        _x = _y;
                        _y = _z;
                        _c = t >> 32;
                        _z = t & 0xffffffff;
                        uint uint32 = (uint)_z;
                        int31 = (int)(uint32 >> 1);
                    } while (int31 == int.MaxValue);
                    return int31;
                }
            }

            #endregion
        }

        #endregion

        #region Constructors

        public Arbitrary(Algorithms.Integer algorithm)
        {
            this._wrapper = new IntegerWrapper(algorithm);
        }

        public Arbitrary(Algorithms.Double algorithm)
        {
            this._wrapper = new DoubleWrapper(algorithm);
        }

        #endregion

        #region Operators

        public static implicit operator Arbitrary(Algorithms.Integer algorithm) { return new Arbitrary(algorithm); }
        public static implicit operator Arbitrary(Algorithms.Double algorithm) { return new Arbitrary(algorithm); }

        #endregion

        #region Overrides

        public override int Next() { return this._wrapper.Next(); }
        public override int Next(int maxValue) { return this._wrapper.Next(maxValue); }
        public override int Next(int minValue, int maxValue) { return this._wrapper.Next(minValue, maxValue); }
        public override void NextBytes(byte[] buffer) { this._wrapper.NextBytes(buffer); }
        public override double NextDouble() { return this._wrapper.NextDouble(); }
        protected override double Sample() { return this._wrapper.NextDouble(); }

        #endregion
    }
}
