namespace Towel.Measurements
{
    /// <summary>A delegate representing a conversion function from one unit to another.</summary>
    /// <typeparam name="T">The numeric type of the conversion.</typeparam>
    /// <param name="value">The value to be converted.</param>
    /// <returns>The value after its conversion to the new unit type.</returns>
    internal delegate T UnitConversion<T>(T value);
}
