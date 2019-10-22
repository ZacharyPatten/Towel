using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Towel.Mathematics;
using static Towel.Syntax;

namespace Towel.Physics.Shapes
{
    public static class ShapeOverlapDetection<T>
    {
        public static bool Detect(Shape<T> a, Shape<T> b, out Vector<T> point, out Vector<T> normal, out T penetration, int xenoMaxIterations = DefaultMaxXenoIterations)
        {
            point = null;
            normal = null;
            penetration = default(T);

            // Step 1: Roughly Estimated Bounding-Box Collisions
            Bounds<T> a_bounds = a.RoughBounds;
            Bounds<T> b_bounds = b.RoughBounds;
            if (!BoundsDetection(a_bounds, b_bounds))
            {
                return false;
            }

            // Step 2: Known Optimized Shape Combinations (Optimizations)
            if (a is Sphere<T> && b is Sphere<T>)
            {
                return SphereDetection(a as Sphere<T>, b as Sphere<T>, out point, out normal, out penetration);
            }

            // Step 3: Use the Xeno algorithm
            if (a is XenoScan<T> && b is XenoScan<T>)
            {
                return XenoDetection(a as XenoScan<T>, b as XenoScan<T>, xenoMaxIterations, out point, out normal, out penetration);
            }

            // need more complex collision detection
            throw new System.NotImplementedException();
        }

        private static bool BoundsDetection(Bounds<T> a, Bounds<T> b)
        {
            bool no_collision =
                // X dimension
                LessThanOrEqual(a.Max.X, b.Min.X) ||
                GreaterThanOrEqual(a.Min.X, b.Max.X) ||
                // Y dimension
                LessThanOrEqual(a.Max.Y, b.Min.Y) ||
                GreaterThanOrEqual(a.Min.Y, b.Max.Y) ||
                // Z dimension
                LessThanOrEqual(a.Max.Z, b.Min.Z) ||
                GreaterThanOrEqual(a.Min.Z, b.Max.Z);

            return !no_collision;
        }

        private static bool SphereDetection(Sphere<T> a, Sphere<T> b, out Vector<T> point, out Vector<T> normal, out T penetration)
        {
            Vector<T> b_minus_a = b.Position - a.Position;
            T distance = b_minus_a.Magnitude;
            T combinedRadius = Addition(a.Radius, b.Radius);

            if (LessThan(distance, combinedRadius))
            {
                penetration = Subtraction(combinedRadius, distance);
                normal = b_minus_a.Normalize();
                point = (b_minus_a * Division(Constant<T>.One, Constant<T>.Two)) + a.Position;
                return true;
            }
            else
            {
                penetration = default(T);
                normal = null;
                point = null;
                return false;
            }
        }

        private static bool XenoDetection(
            XenoScan<T> a,
            XenoScan<T> b,
            int maxIterations,
            out Vector<T> point,
            out Vector<T> normal,
            out T penetration)
        {
            point = null;
            normal = null;
            penetration = default(T);

            Vector<T> minkowskiDifference = b.Position - a.Position;
            normal = -minkowskiDifference;

            if (NearlyZero(minkowskiDifference.MagnitudeSquared))
                minkowskiDifference = new Vector<T>(
                    Division(Constant<T>.One, Convert<int, T>(100000)),
                    Constant<T>.Zero,
                    Constant<T>.Zero);
            
            Vector<T> a_xenoScan1 = Quaternion<T>.Rotate(a.Orientation, a.XenoScan(Quaternion<T>.Rotate(a.Orientation, minkowskiDifference)));
            Vector<T> b_xenoScan1 = Quaternion<T>.Rotate(b.Orientation, b.XenoScan(Quaternion<T>.Rotate(b.Orientation, normal)));
            Vector<T> xenoScans1_subtract = b_xenoScan1 - a_xenoScan1;

            if (LessThanOrEqual(Vector<T>.DotProduct(xenoScans1_subtract, normal), Constant<T>.Zero))
            {
                return false;
            }

            Vector<T> crossOfXenoAndMD = Vector<T>.CrossProduct(xenoScans1_subtract, minkowskiDifference);
            
            if (NearlyZero(crossOfXenoAndMD.MagnitudeSquared))
            {
                crossOfXenoAndMD = (xenoScans1_subtract - minkowskiDifference).Normalize();
                point = (a_xenoScan1 + b_xenoScan1) * Division(Constant<T>.One, Constant<T>.Two);
                penetration = Vector<T>.DotProduct(xenoScans1_subtract, crossOfXenoAndMD);
                return true;
            }

            Vector<T> a_xenoScan2 = Quaternion<T>.Rotate(a.Orientation, a.XenoScan(Quaternion<T>.Rotate(a.Orientation, -crossOfXenoAndMD)));
            Vector<T> b_xenoScan2 = Quaternion<T>.Rotate(b.Orientation, b.XenoScan(Quaternion<T>.Rotate(b.Orientation, normal)));
            Vector<T> xenoScans2_subtract = b_xenoScan2 - a_xenoScan2;

            if (LessThanOrEqual(Vector<T>.DotProduct(xenoScans2_subtract, normal), Constant<T>.Zero))
            {
                return false;
            }

            Vector<T> crossOfXenoScans = Vector<T>.CrossProduct(xenoScans1_subtract - minkowskiDifference, xenoScans2_subtract - minkowskiDifference);

            T distance = Vector<T>.DotProduct(crossOfXenoAndMD, minkowskiDifference);

            if (LessThan(distance, Constant<T>.Zero))
            {
                Vector<T> temp;

                temp = xenoScans1_subtract;
                xenoScans1_subtract = xenoScans2_subtract;
                xenoScans2_subtract = temp;

                temp = a_xenoScan1;
                a_xenoScan1 = a_xenoScan2;
                a_xenoScan2 = temp;

                temp = b_xenoScan1;
                b_xenoScan1 = b_xenoScan2;
                b_xenoScan2 = temp;

                normal = -normal;
            }

            int phase2 = 0;
            int phase1 = 0;
            bool hit = false;

            while (true)
            {
                if (phase1 > maxIterations)
                    return false;

                phase1++;

                Vector<T> neg_normal = -normal;

                Vector<T> a_xenoScan3 = Quaternion<T>.Rotate(a.Orientation, a.XenoScan(Quaternion<T>.Rotate(a.Orientation, neg_normal)));
                Vector<T> b_xenoScan3 = Quaternion<T>.Rotate(b.Orientation, b.XenoScan(Quaternion<T>.Rotate(b.Orientation, normal)));
                Vector<T> xenoScans3_subtract = b_xenoScan3 - a_xenoScan3;

                if (LessThan(Vector<T>.DotProduct(xenoScans3_subtract, normal), Constant<T>.Zero))
                    return false;

                if (LessThan(Vector<T>.DotProduct(Vector<T>.CrossProduct(xenoScans1_subtract, xenoScans3_subtract), minkowskiDifference), Constant<T>.Zero))
                {
                    xenoScans2_subtract = xenoScans3_subtract;
                    a_xenoScan2 = a_xenoScan3;
                    b_xenoScan2 = b_xenoScan3;
                    normal = Vector<T>.CrossProduct(xenoScans1_subtract - minkowskiDifference, xenoScans3_subtract - minkowskiDifference);
                    continue;
                }

                if (LessThan(Vector<T>.DotProduct(Vector<T>.CrossProduct(xenoScans3_subtract, xenoScans2_subtract), minkowskiDifference), Constant<T>.Zero))
                {
                    xenoScans1_subtract = xenoScans3_subtract;
                    a_xenoScan1 = a_xenoScan3;
                    b_xenoScan1 = b_xenoScan3;
                    normal = Vector<T>.CrossProduct(xenoScans3_subtract - minkowskiDifference, xenoScans2_subtract - minkowskiDifference);
                    continue;
                }

                while (true)
                {
                    phase2++;

                    normal = Vector<T>.CrossProduct(xenoScans2_subtract - xenoScans1_subtract, xenoScans3_subtract - xenoScans1_subtract);

                    // Ommited because appears to be an error
                    //if (NearlyZero(normal.Magnitude()))
                    //    return true;

                    normal = normal.Normalize();

                    if (!hit && GreaterThanOrEqual(Vector<T>.DotProduct(normal, xenoScans1_subtract), Constant<T>.Zero))
                    {
                        hit = true;
                    }

                    neg_normal = -normal;

                    Vector<T> a_xenoScan4 = Quaternion<T>.Rotate(a.Orientation, a.XenoScan(Quaternion<T>.Rotate(a.Orientation, neg_normal)));
                    Vector<T> b_xenoScan4 = Quaternion<T>.Rotate(b.Orientation, b.XenoScan(Quaternion<T>.Rotate(b.Orientation, normal)));
                    Vector<T> xenoScans4_subtract = b_xenoScan4 - a_xenoScan4;

                    T delta = Vector<T>.DotProduct(xenoScans4_subtract - xenoScans3_subtract, normal);
                    penetration = Vector<T>.DotProduct(xenoScans4_subtract, normal);

                    // If the boundary is thin enough or the origin is outside the support plane for the newly discovered vertex, then we can terminate
                    if (LessThanOrEqual(delta, CollideEpsilon) || LessThanOrEqual(penetration, Constant<T>.Zero) || phase2 > maxIterations)
                    {

                        if (hit)
                        {
                            T b0 = Vector<T>.DotProduct(Vector<T>.CrossProduct(xenoScans1_subtract, xenoScans2_subtract), xenoScans3_subtract);
                            T b1 = Vector<T>.DotProduct(Vector<T>.CrossProduct(xenoScans3_subtract, xenoScans2_subtract), minkowskiDifference);
                            T b2 = Vector<T>.DotProduct(Vector<T>.CrossProduct(minkowskiDifference, xenoScans1_subtract), xenoScans3_subtract);
                            T b3 = Vector<T>.DotProduct(Vector<T>.CrossProduct(xenoScans2_subtract, xenoScans1_subtract), minkowskiDifference);

                            T sum = Addition(b0, b1, b2, b3);

                            if (LessThanOrEqual(sum, Constant<T>.Zero))
                            {
                                b0 = Constant<T>.Zero;
                                b1 = Vector<T>.DotProduct(Vector<T>.CrossProduct(xenoScans2_subtract, xenoScans3_subtract), normal);
                                b2 = Vector<T>.DotProduct(Vector<T>.CrossProduct(xenoScans3_subtract, xenoScans1_subtract), normal);
                                b3 = Vector<T>.DotProduct(Vector<T>.CrossProduct(xenoScans1_subtract, xenoScans2_subtract), normal);

                                sum = Addition(b0, b1, b2, b3);
                            }

                            T inv = Division(Constant<T>.One, sum);

                            point =
                                (a.Position * b0 +
                                a_xenoScan1 * b1 +
                                a_xenoScan2 * b2 +
                                a_xenoScan3 * b3 +

                                b.Position * b0 +
                                b_xenoScan1 * b1 +
                                b_xenoScan2 * b2 +
                                b_xenoScan3 * b3)
                                * inv;
                        }

                        return hit;
                    }

                    Vector<T> xeno4CrossMD = Vector<T>.CrossProduct(xenoScans4_subtract, minkowskiDifference);
                    T dot = Vector<T>.DotProduct(xeno4CrossMD, xenoScans1_subtract);

                    if (GreaterThanOrEqual(dot, Constant<T>.Zero))
                    {
                        dot = Vector<T>.DotProduct(xeno4CrossMD, xenoScans2_subtract);

                        if (GreaterThanOrEqual(dot, Constant<T>.Zero))
                        {
                            xenoScans1_subtract = xenoScans4_subtract;
                            a_xenoScan1 = a_xenoScan4;
                            b_xenoScan1 = b_xenoScan4;
                        }
                        else
                        {
                            xenoScans3_subtract = xenoScans4_subtract;
                            a_xenoScan3 = a_xenoScan4;
                            b_xenoScan3 = b_xenoScan4;
                        }
                    }
                    else
                    {
                        dot = Vector<T>.DotProduct(xeno4CrossMD, xenoScans3_subtract);

                        if (GreaterThanOrEqual(dot, Constant<T>.Zero))
                        {
                            xenoScans2_subtract = xenoScans4_subtract;
                            a_xenoScan2 = a_xenoScan4;
                            b_xenoScan2 = b_xenoScan4;
                        }
                        else
                        {
                            xenoScans1_subtract = xenoScans4_subtract;
                            a_xenoScan1 = a_xenoScan4;
                            b_xenoScan1 = b_xenoScan4;
                        }
                    }
                }
            }
        }

        #region XenoDetection Helpers

        private const int DefaultMaxXenoIterations = 34;
        private static T CollideEpsilon = Division(Constant<T>.One, Convert<int, T>(10000));

        private static bool NearlyZero(T value)
        {
            throw new NotImplementedException();
            //return LessThan(value, Power(Constant<T>.Epsilon, Compute.FromInt32<T>(2)));
        }

        #endregion

        public static bool CheckLineBox<T>(Bounds<T> bounds, Vector<T> a, Vector<T> b, out Vector<T> hit)
        {
            hit = null;
            int dimensions = bounds.Min.Dimensions;
            for (int i = 0; i < dimensions; i++)
            {
                if (LessThan(b[i], bounds.Min[i]) && LessThan(a[i], bounds.Min[i]))
                    return false;
                if (GreaterThan(b[i], bounds.Max[i]) && GreaterThan(a[i], bounds.Max[i]))
                    return false;
            }

            bool temp = true;
            for (int i = 0; i < dimensions; i++)
            {
                if (!(GreaterThan(a[i], bounds.Min[i]) && LessThan(a[i], bounds.Max[i])))
                {
                    temp = false;
                    break;
                }
            }
            if (temp == true)
            {
                hit = a;
                return true;
            }

            Bounds<T> A_B = new Bounds<T>(a, b);

            if ((GetIntersection(Subtraction(a[0], bounds.Min[0]), Subtraction(b[0], bounds.Min[0]), A_B, out hit) && InBox(hit, bounds, 1)) ||
                (GetIntersection(Subtraction(a[1], bounds.Min[1]), Subtraction(b[1], bounds.Min[1]), A_B, out hit) && InBox(hit, bounds, 2)) ||
                (GetIntersection(Subtraction(a[2], bounds.Min[2]), Subtraction(b[2], bounds.Min[2]), A_B, out hit) && InBox(hit, bounds, 3)) ||
                (GetIntersection(Subtraction(a[0], bounds.Max[0]), Subtraction(b[0], bounds.Max[0]), A_B, out hit) && InBox(hit, bounds, 1)) ||
                (GetIntersection(Subtraction(a[1], bounds.Max[1]), Subtraction(b[1], bounds.Max[1]), A_B, out hit) && InBox(hit, bounds, 2)) ||
                (GetIntersection(Subtraction(a[2], bounds.Max[2]), Subtraction(b[2], bounds.Max[2]), A_B, out hit) && InBox(hit, bounds, 3)))
                return true;

            return false;
        }

        #region Line Box Helper

        private static bool GetIntersection<T>(T fDst1, T fDst2, Bounds<T> bounds, out Vector<T> hit)
        {
            hit = null;
            if (GreaterThanOrEqual(Multiplication(fDst1, fDst2), Constant<T>.Zero))
                return false;
            if (Equality(fDst1, fDst2))
                return false;
            hit = bounds.Min + (bounds.Max - bounds.Min) * Division(Negation(fDst1), (Subtraction(fDst2, fDst1)));
            return true;
        }

        private static bool InBox<T>(Vector<T> hit, Bounds<T> bounds, int axis)
        {
            if (bounds.Min.Dimensions != bounds.Max.Dimensions)
                throw new Exception();

            int dimensions = bounds.Min.Dimensions;

            for (int i = 0; i < dimensions; i++)
            {
                if (axis == 1 && GreaterThan(hit[2], bounds.Min[2]) && LessThan(hit[2], bounds.Max[2]) && GreaterThan(hit[1], bounds.Min[1]) && LessThan(hit[1], bounds.Max[1]))
                    return true;
            }

            if (axis == 1 && GreaterThan(hit[2], bounds.Min[2]) && LessThan(hit[2], bounds.Max[2]) && GreaterThan(hit[1], bounds.Min[1]) && LessThan(hit[1], bounds.Max[1]))
                return true;
            if (axis == 2 && GreaterThan(hit[2], bounds.Min[2]) && LessThan(hit[2], bounds.Max[2]) && GreaterThan(hit[0], bounds.Min[0]) && LessThan(hit[0], bounds.Max[0]))
                return true;
            if (axis == 3 && GreaterThan(hit[0], bounds.Min[0]) && LessThan(hit[0], bounds.Max[0]) && GreaterThan(hit[1], bounds.Min[1]) && LessThan(hit[1], bounds.Max[1]))
                return true;
            return false;
        }

        #endregion
    }
}
