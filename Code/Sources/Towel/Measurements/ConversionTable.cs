using System;
using System.Collections.Generic;
using System.Text;

namespace Towel.Measurements
{
    internal static class ConversionTable
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
}
