using System;
using System.Diagnostics;
using System.Linq;
using Towel.Mathematics;

namespace Towel.Measurements
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    internal class ConversionFactorAttribute : Attribute
    {
        internal readonly Enum To;
        internal readonly string Expression;

        internal ConversionFactorAttribute(object to, string expression)
        {
            if (!(to is Enum))
            {
                throw new ArgumentException("There is a BUG in " + nameof(Towel) + ". A " + nameof(ConversionFactorAttribute) + " contains a non-enum value.", nameof(to));
            }
            To = (Enum)to;
            Expression = expression;
        }

        internal T Value<T>()
        {
            Symbolics.Expression expression = Symbolics.Parse<T>(Expression);
            Symbolics.Constant<T> constant = expression.Simplify() as Symbolics.Constant<T>;
            return constant.Value;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    internal class MetricUnitAttribute : Attribute
    {
        internal readonly MetricUnits MetricUnits;

        internal MetricUnitAttribute(MetricUnits metricUnits)
        {
            MetricUnits = metricUnits;
        }
    }

    internal static class UnitConversionTable
    {
        internal static Func<T, T>[][] Build<UNITS, T>()
        {
            int size = Convert.ToInt32(Extensions.GetMaxEnumValue<UNITS>());
            Func<T, T>[][] conversionFactorTable = Extensions.ConstructSquareJagged<Func<T, T>>(size + 1);
            foreach (Enum A_unit in Enum.GetValues(typeof(UNITS)))
            {
                int A = Convert.ToInt32(A_unit);

                foreach (Enum B_unit in Enum.GetValues(typeof(UNITS)))
                {
                    int B = Convert.ToInt32(B_unit);

                    MetricUnitAttribute A_metric = A_unit.GetEnumAttribute<MetricUnitAttribute>();
                    MetricUnitAttribute B_metric = B_unit.GetEnumAttribute<MetricUnitAttribute>();

                    if (A == B)
                    {
                        conversionFactorTable[A][B] = x => x;
                    }
                    else if (!(A_metric is null) && !(B_metric is null))
                    {
                        int metricDifference = (int)A_metric.MetricUnits - (int)B_metric.MetricUnits;
                        if (metricDifference < 0)
                        {
                            metricDifference = -metricDifference;
                            T factor = Compute.Power(Constant<T>.Ten, Compute.Convert<int, T>(metricDifference));
                            conversionFactorTable[A][B] = x => Compute.Multiply(factor, x);
                        }
                        else
                        {
                            T factor = Compute.Power(Constant<T>.Ten, Compute.Convert<int, T>(metricDifference));
                            conversionFactorTable[A][B] = x => Compute.Multiply(factor, x);
                        }
                    }
                    else if (A < B)
                    {
                        foreach (ConversionFactorAttribute conversionFactor in B_unit.GetEnumAttributes<ConversionFactorAttribute>().Where(c => Convert.ToInt32(c.To) == A))
                        {
                            T factor = conversionFactor.Value<T>();
                            conversionFactorTable[A][B] = x => Compute.Divide(x, factor);
                        }
                    }
                    else if (A > B)
                    {
                        foreach (ConversionFactorAttribute conversionFactor in A_unit.GetEnumAttributes<ConversionFactorAttribute>().Where(c => Convert.ToInt32(c.To) == B))
                        {
                            T factor = conversionFactor.Value<T>();
                            conversionFactorTable[A][B] = x => Compute.Multiply(x, factor);
                        }
                    }
                    else
                    {
                        conversionFactorTable[A][B] = x => throw new Exception("Bug. Encountered an unhandled unit conversion.");
                    }


                    if (conversionFactorTable[A][B] is null)
                    {
                        Type type1 = typeof(UNITS);
                        Type type2 = typeof(T);
                        Debugger.Break();
                    }
                }

                //// handle metric units first
                //MetricUnitAttribute A_metric = A_unit.GetEnumAttribute<MetricUnitAttribute>();
                //if (!(A_metric is null))
                //{
                //    foreach (Enum B_units in Enum.GetValues(typeof(UNITS)))
                //    {
                //        int B = Convert.ToInt32(B_units);

                //        MetricUnitAttribute B_metric = B_units.GetEnumAttribute<MetricUnitAttribute>();
                //        if (!(B_metric is null))
                //        {
                //            int metricDifference = (int)A_metric.MetricUnits - (int)B_metric.MetricUnits;
                //            T factor = Compute.Power(Constant<T>.Ten, Compute.FromInt32<T>(metricDifference));
                //            conversionFactorTable[A][B] = x => Compute.Multiply(factor, x);
                //        }
                //        else
                //        {
                //            foreach (ConversionFactorAttribute conversionFactor in B_units.GetEnumAttributes<ConversionFactorAttribute>())
                //            {
                //                if (conversionFactor.To.Equals(A_unit))
                //                {
                //                    T factor = Compute.Invert(conversionFactor.Value<T>());
                //                    conversionFactorTable[A][B] = x => Compute.Multiply(factor, x);
                //                    break;
                //                }
                //            }
                //        }
                //    }
                //}

                //// handle explicit conversion factors
                //foreach (ConversionFactorAttribute conversionFactor in A_unit.GetEnumAttributes<ConversionFactorAttribute>())
                //{
                //    int B = Convert.ToInt32(conversionFactor.To);
                //    T factor = conversionFactor.Value<T>();
                //    conversionFactorTable[A][B] = x => Compute.Multiply(factor, x);
                //}
            }
            return conversionFactorTable;
        }
    }

    internal enum MetricUnits
    {
        Yocto = -24,
        Zepto = -21,
        Atto = -18,
        Femto = -15,
        Pico = -12,
        Nano = -9,
        Micro = -6,
        Milli = -3,
        Centi = -2,
        Deci = -1,
        BASE = 0,
        Deka = 1,
        Hecto = 2,
        Kilo = 3,
        Mega = 6,
        Giga = 9,
        Tera = 12,
        Peta = 15,
        Exa = 18,
        Zetta = 21,
        Yotta = 24,
    }
}
