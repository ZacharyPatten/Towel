using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
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
            this.To = (Enum)to;
            this.Expression = expression;
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
            this.MetricUnits = metricUnits;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    internal class ComplexUnitNumeratorsAttribute : Attribute
    {
        internal readonly Enum[] NUMERATORS;

        internal ComplexUnitNumeratorsAttribute(object a, params object[] b)
        {
            if (!(a is Enum))
            {
                throw new ArgumentException("There is a BUG in " + nameof(Towel) + ". A " + nameof(ComplexUnitNumeratorsAttribute) + " contains in a non-enum value.", nameof(a));
            }
            foreach (object @object in b)
            {
                if (!(a is Enum))
                {
                    throw new ArgumentException("There is a BUG in " + nameof(Towel) + ". A " + nameof(ComplexUnitNumeratorsAttribute) + " contains in a non-enum value.", nameof(a));
                }
            }
            NUMERATORS = new Enum[b.Length + 1];
            for (int i = 0; i < b.Length; i++)
            {
                NUMERATORS[i] = (Enum)b[i];
            }
            NUMERATORS[b.Length] = (Enum)a;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    internal class ComplexUnitDenominatorsAttribute : Attribute
    {
        internal readonly Enum[] DENOMINATORS;

        internal ComplexUnitDenominatorsAttribute(object a, params object[] b)
        {
            if (!(a is Enum))
            {
                throw new ArgumentException("There is a BUG in " + nameof(Towel) + ". A " + nameof(ComplexUnitNumeratorsAttribute) + " contains in a non-enum value.", nameof(a));
            }
            foreach (object @object in b)
            {
                if (!(a is Enum))
                {
                    throw new ArgumentException("There is a BUG in " + nameof(Towel) + ". A " + nameof(ComplexUnitNumeratorsAttribute) + " contains in a non-enum value.", nameof(a));
                }
            }
            DENOMINATORS = new Enum[b.Length + 1];
            for (int i = 0; i < b.Length; i++)
            {
                DENOMINATORS[i] = (Enum)b[i];
            }
            DENOMINATORS[b.Length] = (Enum)a;
        }
    }

    internal static class UnitConversionTable
    {
        internal static T[][] Build<UNITS, T>()
        {
            int size = Convert.ToInt32(Extensions.GetMaxEnumValue<UNITS>());
            T[][] conversionFactorTable = Extensions.ConstructSquareJagged<T>(size + 1);
            foreach (Enum A_unit in Enum.GetValues(typeof(UNITS)))
            {
                int A = Convert.ToInt32(A_unit);

                // handle metric units first
                MetricUnitAttribute A_metric = A_unit.GetEnumAttribute<MetricUnitAttribute>();
                if (!(A_metric is null))
                {
                    foreach (Enum B_units in Enum.GetValues(typeof(UNITS)))
                    {
                        int B = Convert.ToInt32(B_units);

                        MetricUnitAttribute B_metric = B_units.GetEnumAttribute<MetricUnitAttribute>();
                        if (!(B_metric is null))
                        {
                            int metricDifference = (int)A_metric.MetricUnits - (int)B_metric.MetricUnits;
                            conversionFactorTable[A][B] = Compute.Power(Constant<T>.Ten, Compute.FromInt32<T>(metricDifference));
                        }
                        else
                        {
                            foreach (ConversionFactorAttribute conversionFactor in B_units.GetEnumAttributes<ConversionFactorAttribute>())
                            {
                                if (conversionFactor.To.Equals(A_unit))
                                {
                                    conversionFactorTable[A][B] = Compute.Invert(conversionFactor.Value<T>());
                                    break;
                                }
                            }
                        }
                    }
                }

                // handle explicit conversion factors
                foreach (ConversionFactorAttribute conversionFactor in A_unit.GetEnumAttributes<ConversionFactorAttribute>())
                {
                    int B = Convert.ToInt32(conversionFactor.To);
                    conversionFactorTable[A][B] = conversionFactor.Value<T>();
                }
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

    internal static class MeasurementConversionTable
    {
        internal static class Multiplication
        {
            //internal static C[][] Build<A, B, C>()
            //{
            //    // Notes:
            //    // A = left hand unit type
            //    // B = right hand unit type
            //    // C = resulting unit type (for multiplication)


            //}
        }
        internal static class Division
        {
            //internal static C[][] Build<A, B, C>()
            //{
            //    // Notes:
            //    // A = left hand unit type
            //    // B = right hand unit type
            //    // C = resulting unit type (for division)


            //}
        }
    }
}
