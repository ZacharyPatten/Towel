using System;
using System.Collections.Generic;
using System.Linq;
using Towel.Mathematics;

namespace Towel.Measurements
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    internal class ConversionFactorAttribute : Attribute
    {
        internal Enum A;
        internal Enum B;
        internal string EXPRESSION;

        internal ConversionFactorAttribute(object a, object b, string expression)
        {
            if (!(a is Enum))
            {
                throw new ArgumentException("There is a BUG in " + nameof(Towel) + ". A " + nameof(ConversionFactorAttribute) + " attribute passed in a non-enum value.", nameof(a));
            }
            if (!(b is Enum))
            {
                throw new ArgumentException("There is a BUG in " + nameof(Towel) + ". A " + nameof(ConversionFactorAttribute) + " attribute passed in a non-enum value.", nameof(b));
            }
            this.A = (Enum)a;
            this.B = (Enum)b;
            this.EXPRESSION = expression;
        }
        
        internal T Value<T>()
        {
            try
            {
                Symbolics<T>.Node expression = Symbolics<T>.Parse(EXPRESSION);
                Symbolics<T>.Constant constant = expression.Simplify() as Symbolics<T>.Constant;
                return constant.Value;
            }
            catch (Exception exception)
            {
                throw new Exception("There is a BUG in " + nameof(Towel) + ". A " + nameof(ConversionFactorAttribute) + " expression could not simplify to a constant.", exception);
            }
        }

        internal static ConversionFactorAttribute Get(Enum a, Enum b)
        {
            IEnumerable<ConversionFactorAttribute> attributes = a.GetEnumAttributes<ConversionFactorAttribute>().Where(x => Enum.Equals(x.A, a) && Enum.Equals(x.B, b));
            if (attributes.Count() != 1)
            {
                throw new Exception("There is a BUG in " + nameof(Towel) + ". Missing a conversion factor attribute for " + a.ToString() + " -> " + b.ToString() + ".");
            }
            return attributes.First();
        }
    }
}
