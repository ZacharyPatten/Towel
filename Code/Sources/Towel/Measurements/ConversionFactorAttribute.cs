using System;
using System.Collections.Generic;
using System.Linq;
using Towel.Mathematics;

namespace Towel.Measurements
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    internal class ConversionFactorAttribute : Attribute
    {
        internal Enum To;
        internal string EXPRESSION;

        internal ConversionFactorAttribute(object to, string expression)
        {
            if (!(to is Enum))
            {
                throw new ArgumentException("There is a BUG in " + nameof(Towel) + ". A " + nameof(ConversionFactorAttribute) + " attribute passed in a non-enum value.", nameof(to));
            }
            this.To = (Enum)to;
            this.EXPRESSION = expression;
        }
        
        internal T Value<T>()
        {
            try
            {
                Symbolics.Expression expression = Symbolics.Parse<T>(EXPRESSION);
                Symbolics.Constant<T> constant = expression.Simplify() as Symbolics.Constant<T>;
                return constant.Value;
            }
            catch (Exception exception)
            {
                throw new Exception("There is a BUG in " + nameof(Towel) + ". A " + nameof(ConversionFactorAttribute) + " expression could not simplify to a constant.", exception);
            }
        }
    }
}
