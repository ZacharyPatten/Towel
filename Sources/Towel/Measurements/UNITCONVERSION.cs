using System;
using System.Collections.Generic;
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
            try
            {
                Symbolics.Expression expression = Symbolics.Parse<T>(Expression);
                Symbolics.Constant<T> constant = expression.Simplify() as Symbolics.Constant<T>;
                return constant.Value;
            }
            catch (Exception exception)
            {
                throw new Exception("There is a BUG in " + nameof(Towel) + ". A " + nameof(ConversionFactorAttribute) + " expression could not simplify to a constant.", exception);
            }
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
        internal readonly Enum[] Denominators;

        internal ComplexUnitDenominatorsAttribute(Enum a, params Enum[] b)
        {
            Denominators = new Enum[b.Length + 1];
            for (int i = 0; i < b.Length; i++)
            {
                Denominators[i] = b[i];
            }
            Denominators[b.Length] = a;
        }
    }

    internal static class UnitConversionTable
    {
        internal static T[][] Build<UNITS, T>()
        {
            int size = Convert.ToInt32(Extensions.GetMaxEnumValue<UNITS>());
            T[][] conversionFactorTable = Extensions.ConstructSquareJagged<T>(size);
            foreach (Enum unit in Enum.GetValues(typeof(UNITS)))
            {
                int A = Convert.ToInt32(unit);
                foreach (ConversionFactorAttribute conversionFactor in unit.GetEnumAttributes<ConversionFactorAttribute>())
                {
                    int B = Convert.ToInt32(conversionFactor.To);
                    conversionFactorTable[A][B] = conversionFactor.Value<T>();
                }
            }
            return conversionFactorTable;
        }
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
