using Towel.Mathematics;

namespace Towel.Measurements
{
    public static class Tempurature
    {
        #region Units

        public enum Units
        {
            Celsius, //(denoted °C; formerly called centigrade), 
            Fahrenheit,// (denoted °F), and, especially in science, 
            Kelvin,// (denoted K).
        }

        /// <summary>
        /// Orders all the unit types based on priority. The lower the number the higher 
        /// the priority. The highest priority units are used when running computations
        /// on measurements of different units.
        /// </summary>
        internal static int UnitOrder(Units units)
        {
            switch (units)
            {
                case Tempurature.Units.Fahrenheit:
                    return 0;
                case Tempurature.Units.Celsius:
                    return 1;
                case Tempurature.Units.Kelvin:
                    return 2;
                default:
                    throw new System.NotImplementedException();
            }
        }

        #endregion
    }
}
