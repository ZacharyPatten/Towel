using Towel.Mathematics;

namespace Towel.Measurements
{
	public static class Length<T>
    {
        #region From Meters

        /// <summary>The factor for converting meters to yottameters (10E-24).</summary>
        public static T MetersToYottametersFactor { get { return Metric<T>.BaseToYottaFactor; } }
        /// <summary>Converts meters to yottameters.</summary>
        /// <param name="measurement">The measurement in meters to convert to yottameters.</param>
        /// <returns>The measurement in yottameters.</returns>
        public static T MetersToYottameters(T measurement) { return Metric<T>.BaseToYotta(measurement); }

        /// <summary>The factor for converting meters to zettameters (10E-21).</summary>
        public static T MetersToZettametersFactor { get { return Metric<T>.BaseToZettaFactor; } }
        /// <summary>Converts meters to zettameters.</summary>
        /// <param name="measurement">The measurement in meters to convert to zettameters.</param>
        /// <returns>The measurement in zettameters.</returns>
        public static T MetersToZettameters(T measurement) { return Metric<T>.BaseToZetta(measurement); }

        /// <summary>The factor for converting meters to exameters (10E-21).</summary>
        public static T MetersToExametersFactor { get { return Metric<T>.BaseToExaFactor; } }
        /// <summary>Converts meters to exameters.</summary>
        /// <param name="measurement">The measurement in meters to convert to exameters.</param>
        /// <returns>The measurement in exaameters.</returns>
        public static T MetersToExameters(T measurement) { return Metric<T>.BaseToExa(measurement); }

        /// <summary>The factor for converting meters to petameters (10E-21).</summary>
        public static T MetersToPetametersFactor { get { return Metric<T>.BaseToPetaFactor; } }
        public static T MetersToPetameters(T measurement) { return Metric<T>.BaseToPeta(measurement); }

        /// <summary>The factor for converting meters to terameters (10E-21).</summary>
        public static T MetersToTerametersFactor { get { return Metric<T>.BaseToTeraFactor; } }
        public static T MetersToTerameters(T measurement) { return Metric<T>.BaseToTera(measurement); }

        /// <summary>The factor for converting meters to gigameters (10E-9).</summary>
        public static T MetersToGigametersFactor { get { return Metric<T>.BaseToGigaFactor; } }
        public static T MetersToGigameters(T measurement) { return Metric<T>.BaseToGiga(measurement); }

        /// <summary>The factor for converting meters to gigameters (10E-6).</summary>
        public static T MetersToMegametersFactor { get { return Metric<T>.BaseToMegaFactor; } }
        public static T MetersToMegameters(T measurement) { return Metric<T>.BaseToMega(measurement); }

        /// <summary>The factor for converting meters to kilometers (10E-3).</summary>
        public static T MetersToKilometersFactor { get { return Metric<T>.BaseToKiloFactor; } }
        public static T MetersToKilometers(T measurement) { return Metric<T>.BaseToKilo(measurement); }

        /// <summary>The factor for converting meters to hectometers (10E-2).</summary>
        public static T MetersToHectometersFactor { get { return Metric<T>.BaseToHectoFactor; } }
        public static T MetersToHectometers(T measurement) { return Metric<T>.BaseToHecto(measurement); }

        /// <summary>The factor for converting meters to decameters (10E-1).</summary>
        public static T MetersToDecametersFactor { get { return Metric<T>.BaseToDecaFactor; } }
        public static T MetersToDecameters(T measurement) { return Metric<T>.BaseToDeca(measurement); }
        
        /// <summary>The factor for converting meters to decimeters (10E1).</summary>
        public static T MetersToDecimetersFactor { get { return Metric<T>.BaseToDeciFactor; } }
        public static T MetersToDecimeters(T measurement) { return Metric<T>.BaseToDeci(measurement); }

        /// <summary>The factor for converting meters to centimeters (10E2).</summary>
        public static T MetersToCentimetersFactor { get { return Metric<T>.BaseToCentiFactor; } }
        public static T MetersToCentimeters(T measurement) { return Metric<T>.BaseToCenti(measurement); }

        /// <summary>The factor for converting meters to millimeters (10E3).</summary>
        public static T MetersToMillimetersFactor { get { return Metric<T>.BaseToMilliFactor; } }
        public static T MetersToMillimeters(T measurement) { return Metric<T>.BaseToMilli(measurement); }

        /// <summary>The factor for converting meters to micrometers (10E6).</summary>
        public static T MetersToMicrometersFactor { get { return Metric<T>.BaseToMicroFactor; } }
        public static T MetersToMicrometers(T measurement) { return Metric<T>.BaseToMicro(measurement); }

        /// <summary>The factor for converting meters to nanometers (10E9).</summary>
        public static T MetersToNanometersFactor { get { return Metric<T>.BaseToNanoFactor; } }
        public static T MetersToNanometers(T measurement) { return Metric<T>.BaseToNano(measurement); }

        /// <summary>The factor for converting meters to picometers (10E12).</summary>
        public static T MetersToPicometersFactor { get { return Metric<T>.BaseToPicoFactor; } }
        public static T MetersToPicometers(T measurement) { return Metric<T>.BaseToPico(measurement); }

        /// <summary>The factor for converting meters to femtometers (10E15).</summary>
        public static T MetersToFemtometersFactor { get { return Metric<T>.BaseToFemtoFactor; } }
        public static T MetersToFemtometers(T measurement) { return Metric<T>.BaseToFemto(measurement); }

        /// <summary>The factor for converting meters to attometers (10E18).</summary>
        public static T MetersToAttometersFactor { get { return Metric<T>.BaseToAttoFactor; } }
        public static T MetersToAttometers(T measurement) { return Metric<T>.BaseToAtto(measurement); }

        /// <summary>The factor for converting meters to zeptometers (10E21).</summary>
        public static T MetersToZeptometersFactor { get { return Metric<T>.BaseToZeptoFactor; } }
        public static T MetersToZeptometers(T measurement) { return Metric<T>.BaseToAtto(measurement); }

        /// <summary>The factor for converting meters to yoctometers (10E24).</summary>
        public static T MetersToYoctometersFactor { get { return Metric<T>.BaseToYoctoFactor; } }
        public static T MetersToYoctometers(T measurement) { return Metric<T>.BaseToYocto(measurement); }

        private static bool _metersToThousFactorComputed = false;
        private static T _metersToThousFactor;
        /// <summary>The factor for converting meters to XXX.</summary>
        public static T MetersToThousFactor
        {
            get
            {
                if (!_metersToThousFactorComputed)
                {
                    _metersToThousFactor = Compute.Invert(Compute.Divide(Compute.FromInt32<T>(254000), Compute.FromInt32<T>(10000)));
                    _metersToThousFactorComputed = true;
                }
                return _metersToThousFactor;
            }
        }
        public static T MetersToThous(T measurement) { return Compute.Multiply(measurement, MetersToThousFactor); }

        private static bool _metersToLinesFactorComputed = false;
        private static T _metersToLinesFactor;
        /// <summary>The factor for converting meters to XXX.</summary>
        public static T MetersToLinesFactor
        {
            get
            {
                if (!_metersToLinesFactorComputed)
                {
                    _metersToLinesFactor = Compute.Divide(Compute.FromInt32<T>(47244), Compute.FromInt32<T>(100));
                    _metersToLinesFactorComputed = true;
                }
                return _metersToLinesFactor;
            }
        }

        private static bool _metersToInchsFactorComputed = false;
        private static T _metersToInchsFactor;
        /// <summary>The factor for converting meters to XXX.</summary>
        public static T MetersToInchsFactor
        {
            get
            {
                if (!_metersToInchsFactorComputed)
                {
                    _metersToInchsFactor = Compute.Invert(Compute.Divide(Compute.FromInt32<T>(254), Compute.FromInt32<T>(10000)));
                    _metersToInchsFactorComputed = true;
                }
                return _metersToInchsFactor;
            }
        }

        private static bool _metersToFeetFactorComputed = false;
        private static T _metersToFeetFactor;
        /// <summary>The factor for converting meters to XXX.</summary>
        public static T MetersToFeetFactor
        {
            get
            {
                if (!_metersToFeetFactorComputed)
                {
                    _metersToFeetFactor = Compute.Invert(Compute.Divide(Compute.FromInt32<T>(3048), Compute.FromInt32<T>(10000)));
                    _metersToFeetFactorComputed = true;
                }
                return _metersToFeetFactor;
            }
        }

        private static bool _metersToYardsFactorComputed = false;
        private static T _metersToYardsFactor;
        /// <summary>The factor for converting meters to XXX.</summary>
        public static T MetersToYardsFactor
        {
            get
            {
                if (!_metersToYardsFactorComputed)
                {
                    _metersToYardsFactor = Compute.Invert(Compute.Divide(Compute.FromInt32<T>(9144), Compute.FromInt32<T>(10000)));
                    _metersToYardsFactorComputed = true;
                }
                return _metersToYardsFactor;
            }
        }

        //public static readonly T MetersToMilesFactor = throw new System.NotImplementedException();
        //public static readonly T MetersToLeaguesFactor = ;
        //public static readonly T MetersToFathomsFactor = ;
        //public static readonly T MetersToNauticalMilesFactor = ;
        //public static readonly T MetersToChainsFactor = ;
        //public static readonly T MetersToRodsFactor = ;
        //public static readonly T MetersToEarthRadiusesFactor = ;
        //public static readonly T MetersToLunarDistancesFactor = ;
        //public static readonly T MetersToAstronomicalUnitsFactor = ;
        //public static readonly T MetersToLightYearsFactor = ;
        //public static readonly T MetersToParsecsFactor = ;
        //public static readonly T MetersToHubbleLengthsFactor = ;
        //public static readonly T MetersToElectronRadiusesFactor = ;
        //public static readonly T MetersToComptonWavelengthOfTheElectronsFactor = ;
        //public static readonly T MetersToReducedComptonWavelengthOfTheElectronsFactor = ;
        //public static readonly T MetersToBohrRadiusOfTheHydrogenAtomsFactor = ;
        //public static readonly T MetersToReducedQavelengthOfHydrogenRadiationsFactor = ;
        //public static readonly T MetersToPlanckLengthsFactor = ;
        //public static readonly T MetersToStoneyUnitOfLengthsFactor = ;
        //public static readonly T MetersToQuantumChromodynamicsUnitOfLengthsFactor = ;
        //public static readonly T MetersToNaturalUnitsBasedOnTheElectronvoltsFactor = ;
        //public static readonly T MetersToFurlongsFactor = ;
        //public static readonly T MetersToHorseLengthsFactor = ;

        #endregion
    }
}
