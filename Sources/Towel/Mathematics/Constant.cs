namespace Towel.Mathematics
{
    public static class Constant<T>
    {
        /// <summary>Zero [0]</summary>
        public static readonly T Zero = Compute.Convert<int, T>(0);
        /// <summary>One [1]</summary>
        public static readonly T One = Compute.Convert<int, T>(1);
        /// <summary>Two [2]</summary>
        public static readonly T Two = Compute.Convert<int, T>(2);
        /// <summary>Three [3]</summary>
        public static readonly T Three = Compute.Convert<int, T>(3);
        /// <summary>Four [4]</summary>
        public static readonly T Four = Compute.Convert<int, T>(4);
        /// <summary>Ten [10]</summary>
        public static readonly T Ten = Compute.Convert<int, T>(10);
        /// <summary>Negative One [-1]</summary>
        public static readonly T NegativeOne = Compute.Convert<int, T>(-1);

        /// <summary>π [3.14...]</summary>
        public static readonly T Pi = Compute.Pi<T>();
        /// <summary>π [3.14...]</summary>
        public static readonly T π = Pi;
        /// <summary>2π [6.28...]</summary>
        public static readonly T Pi2 = Compute.Multiply(Two, Pi);
        /// <summary>2π [6.28...]</summary>
        public static readonly T π2 = Pi2;
        /// <summary>π / 2</summary>
        public static readonly T PiOver2 = Compute.Divide(Pi, Two);
        /// <summary>π / 2</summary>
        public static readonly T πOver2 = PiOver2;
        /// <summary>3π/2</summary>
        public static readonly T Pi3Over2 = Compute.Divide(Compute.Multiply(Three, Pi), Two);
        /// <summary>3π/2</summary>
        public static readonly T π3Over2 = Pi3Over2;
        /// <summary>4/(π^2)</summary>
        public static readonly T FourOverPiSquared = Compute.Divide(Compute.Multiply(Three, Pi), Two);
        /// <summary>4/(π^2)</summary>
        public static readonly T FourOverπSquared = FourOverPiSquared;
        /// <summary>-4/(π^2)</summary>
        public static readonly T Negative4OverPiSquared = Compute.Negate(FourOverPiSquared);
        /// <summary>-4/(π^2)</summary>
        public static readonly T Negative4OverπSquared = Negative4OverPiSquared;

        /// <summary>GoldenRatio [(1 + SquareRoot(5)) / 2]</summary>
        //public static readonly T GoldenRatio = Symbolics.ParseAndSimplifyToConstant<T>("(1 + SquareRoot(5)) / 2");

        /// <summary>Epsilon (1.192092896...e-012f)</summary>
        //public static readonly T Epsilon = Compute.ComputeEpsilon<T>();
    }

    #region Old Reference Code

    #region Mathematical Constants

    ///// <summary>The number e</summary>
    //public const double E = 2.7182818284590452353602874713526624977572470937000d;

    ///// <summary>The number ln(10)/20 - factor to convert from Power Decibel (dB) to Neper (Np). Use this version when the Decibel represent a power gain but the compared values are not powers (e.g. amplitude, current, voltage).</summary>
    //public const double PowerDecibel = 0.11512925464970228420089957273421821038005507443144d;

    ///// <summary>The number ln(10)/10 - factor to convert from Neutral Decibel (dB) to Neper (Np). Use this version when either both or neither of the Decibel and the compared values represent powers.</summary>
    //public const double NeutralDecibel = 0.23025850929940456840179914546843642076011014886288d;

    ///// <summary>The Catalan constant</summary>
    ///// <remarks>Sum(k=0 -> inf){ (-1)^k/(2*k + 1)2 }</remarks>
    //public const double Catalan = 0.9159655941772190150546035149323841107741493742816721342664981196217630197762547694794d;

    ///// <summary>The Euler-Mascheroni constant</summary>
    ///// <remarks>lim(n -> inf){ Sum(k=1 -> n) { 1/k - log(n) } }</remarks>
    //public const double EulerMascheroni = 0.5772156649015328606065120900824024310421593359399235988057672348849d;

    ///// <summary>The Glaisher constant</summary>
    ///// <remarks>e^(1/12 - Zeta(-1))</remarks>
    //public const double Glaisher = 1.2824271291006226368753425688697917277676889273250011920637400217404063088588264611297d;

    ///// <summary>The Khinchin constant</summary>
    ///// <remarks>prod(k=1 -> inf){1+1/(k*(k+2))^log(k,2)}</remarks>
    //public const double Khinchin = 2.6854520010653064453097148354817956938203822939944629530511523455572188595371520028011d;

    #endregion

    #region UNIVERSAL CONSTANTS

    ///// <summary>Speed of Light in Vacuum: c_0 = 2.99792458e8 [m s^-1] (defined, exact; 2007 CODATA)</summary>
    //public const double SpeedOfLight = 2.99792458e8;

    ///// <summary>Magnetic Permeability in Vacuum: mu_0 = 4*Pi * 10^-7 [N A^-2 = kg m A^-2 s^-2] (defined, exact; 2007 CODATA)</summary>
    //public const double MagneticPermeability = 1.2566370614359172953850573533118011536788677597500e-6;

    ///// <summary>Electric Permittivity in Vacuum: epsilon_0 = 1/(mu_0*c_0^2) [F m^-1 = A^2 s^4 kg^-1 m^-3] (defined, exact; 2007 CODATA)</summary>
    //public const double ElectricPermittivity = 8.8541878171937079244693661186959426889222899381429e-12;

    ///// <summary>Characteristic Impedance of Vacuum: Z_0 = mu_0*c_0 [Ohm = m^2 kg s^-3 A^-2] (defined, exact; 2007 CODATA)</summary>
    //public const double CharacteristicImpedanceVacuum = 376.73031346177065546819840042031930826862350835242;

    ///// <summary>Newtonian Constant of Gravitation: G = 6.67429e-11 [m^3 kg^-1 s^-2] (2007 CODATA)</summary>
    //public const double GravitationalConstant = 6.67429e-11;

    ///// <summary>Planck's constant: h = 6.62606896e-34 [J s = m^2 kg s^-1] (2007 CODATA)</summary>
    //public const double PlancksConstant = 6.62606896e-34;

    ///// <summary>Reduced Planck's constant: h_bar = h / (2*Pi) [J s = m^2 kg s^-1] (2007 CODATA)</summary>
    //public const double DiracsConstant = 1.054571629e-34;

    ///// <summary>Planck mass: m_p = (h_bar*c_0/G)^(1/2) [kg] (2007 CODATA)</summary>
    //public const double PlancksMass = 2.17644e-8;

    ///// <summary>Planck temperature: T_p = (h_bar*c_0^5/G)^(1/2)/k [K] (2007 CODATA)</summary>
    //public const double PlancksTemperature = 1.416786e32;

    ///// <summary>Planck length: l_p = h_bar/(m_p*c_0) [m] (2007 CODATA)</summary>
    //public const double PlancksLength = 1.616253e-35;

    ///// <summary>Planck time: t_p = l_p/c_0 [s] (2007 CODATA)</summary>
    //public const double PlancksTime = 5.39124e-44;

    #endregion

    #region ELECTROMAGNETIC CONSTANTS

    ///// <summary>Elementary Electron Charge: e = 1.602176487e-19 [C = A s] (2007 CODATA)</summary>
    //public const double ElementaryCharge = 1.602176487e-19;

    ///// <summary>Magnetic Flux Quantum: Towel_0 = h/(2*e) [Wb = m^2 kg s^-2 A^-1] (2007 CODATA)</summary>
    //public const double MagneticFluxQuantum = 2.067833668e-15;

    ///// <summary>Conductance Quantum: G_0 = 2*e^2/h [S = m^-2 kg^-1 s^3 A^2] (2007 CODATA)</summary>
    //public const double ConductanceQuantum = 7.7480917005e-5;

    ///// <summary>Josephson Constant: K_J = 2*e/h [Hz V^-1] (2007 CODATA)</summary>
    //public const double JosephsonConstant = 483597.891e9;

    ///// <summary>Von Klitzing Constant: R_K = h/e^2 [Ohm = m^2 kg s^-3 A^-2] (2007 CODATA)</summary>
    //public const double VonKlitzingConstant = 25812.807557;

    ///// <summary>Bohr Magneton: mu_B = e*h_bar/2*m_e [J T^-1] (2007 CODATA)</summary>
    //public const double BohrMagneton = 927.400915e-26;

    ///// <summary>Nuclear Magneton: mu_N = e*h_bar/2*m_p [J T^-1] (2007 CODATA)</summary>
    //public const double NuclearMagneton = 5.05078324e-27;

    #endregion

    #region ATOMIC AND NUCLEAR CONSTANTS

    ///// <summary>Fine Structure Constant: alpha = e^2/4*Pi*e_0*h_bar*c_0 [1] (2007 CODATA)</summary>
    //public const double FineStructureConstant = 7.2973525376e-3;

    ///// <summary>Rydberg Constant: R_infty = alpha^2*m_e*c_0/2*h [m^-1] (2007 CODATA)</summary>
    //public const double RydbergConstant = 10973731.568528;

    ///// <summary>Bor Radius: a_0 = alpha/4*Pi*R_infty [m] (2007 CODATA)</summary>
    //public const double BohrRadius = 0.52917720859e-10;

    ///// <summary>Hartree Energy: E_h = 2*R_infty*h*c_0 [J] (2007 CODATA)</summary>
    //public const double HartreeEnergy = 4.35974394e-18;

    ///// <summary>Quantum of Circulation: h/2*m_e [m^2 s^-1] (2007 CODATA)</summary>
    //public const double QuantumOfCirculation = 3.6369475199e-4;

    ///// <summary>Fermi Coupling Constant: G_F/(h_bar*c_0)^3 [GeV^-2] (2007 CODATA)</summary>
    //public const double FermiCouplingConstant = 1.16637e-5;

    ///// <summary>Weak Mixin Angle: sin^2(Towel_W) [1] (2007 CODATA)</summary>
    //public const double WeakMixingAngle = 0.22256;

    ///// <summary>Electron Mass: [kg] (2007 CODATA)</summary>
    //public const double ElectronMass = 9.10938215e-31;

    ///// <summary>Electron Mass Energy Equivalent: [J] (2007 CODATA)</summary>
    //public const double ElectronMassEnergyEquivalent = 8.18710438e-14;

    ///// <summary>Electron Molar Mass: [kg mol^-1] (2007 CODATA)</summary>
    //public const double ElectronMolarMass = 5.4857990943e-7;

    ///// <summary>Electron Compton Wavelength: [m] (2007 CODATA)</summary>
    //public const double ComptonWavelength = 2.4263102175e-12;

    ///// <summary>Classical Electron Radius: [m] (2007 CODATA)</summary>
    //public const double ClassicalElectronRadius = 2.8179402894e-15;

    ///// <summary>Tomson Cross Section: [m^2] (2002 CODATA)</summary>
    //public const double ThomsonCrossSection = 0.6652458558e-28;

    ///// <summary>Electron Magnetic Moment: [J T^-1] (2007 CODATA)</summary>
    //public const double ElectronMagneticMoment = -928.476377e-26;

    ///// <summary>Electon G-Factor: [1] (2007 CODATA)</summary>
    //public const double ElectronGFactor = -2.0023193043622;

    ///// <summary>Muon Mass: [kg] (2007 CODATA)</summary>
    //public const double MuonMass = 1.88353130e-28;

    ///// <summary>Muon Mass Energy Equivalent: [J] (2007 CODATA)</summary>
    //public const double MuonMassEnegryEquivalent = 1.692833511e-11;

    ///// <summary>Muon Molar Mass: [kg mol^-1] (2007 CODATA)</summary>
    //public const double MuonMolarMass = 0.1134289256e-3;

    ///// <summary>Muon Compton Wavelength: [m] (2007 CODATA)</summary>
    //public const double MuonComptonWavelength = 11.73444104e-15;

    ///// <summary>Muon Magnetic Moment: [J T^-1] (2007 CODATA)</summary>
    //public const double MuonMagneticMoment = -4.49044786e-26;

    ///// <summary>Muon G-Factor: [1] (2007 CODATA)</summary>
    //public const double MuonGFactor = -2.0023318414;

    ///// <summary>Tau Mass: [kg] (2007 CODATA)</summary>
    //public const double TauMass = 3.16777e-27;

    ///// <summary>Tau Mass Energy Equivalent: [J] (2007 CODATA)</summary>
    //public const double TauMassEnergyEquivalent = 2.84705e-10;

    ///// <summary>Tau Molar Mass: [kg mol^-1] (2007 CODATA)</summary>
    //public const double TauMolarMass = 1.90768e-3;

    ///// <summary>Tau Compton Wavelength: [m] (2007 CODATA)</summary>
    //public const double TauComptonWavelength = 0.69772e-15;

    ///// <summary>Proton Mass: [kg] (2007 CODATA)</summary>
    //public const double ProtonMass = 1.672621637e-27;

    ///// <summary>Proton Mass Energy Equivalent: [J] (2007 CODATA)</summary>
    //public const double ProtonMassEnergyEquivalent = 1.503277359e-10;

    ///// <summary>Proton Molar Mass: [kg mol^-1] (2007 CODATA)</summary>
    //public const double ProtonMolarMass = 1.00727646677e-3;

    ///// <summary>Proton Compton Wavelength: [m] (2007 CODATA)</summary>
    //public const double ProtonComptonWavelength = 1.3214098446e-15;

    ///// <summary>Proton Magnetic Moment: [J T^-1] (2007 CODATA)</summary>
    //public const double ProtonMagneticMoment = 1.410606662e-26;

    ///// <summary>Proton G-Factor: [1] (2007 CODATA)</summary>
    //public const double ProtonGFactor = 5.585694713;

    ///// <summary>Proton Shielded Magnetic Moment: [J T^-1] (2007 CODATA)</summary>
    //public const double ShieldedProtonMagneticMoment = 1.410570419e-26;

    ///// <summary>Proton Gyro-Magnetic Ratio: [s^-1 T^-1] (2007 CODATA)</summary>
    //public const double ProtonGyromagneticRatio = 2.675222099e8;

    ///// <summary>Proton Shielded Gyro-Magnetic Ratio: [s^-1 T^-1] (2007 CODATA)</summary>
    //public const double ShieldedProtonGyromagneticRatio = 2.675153362e8;

    ///// <summary>Neutron Mass: [kg] (2007 CODATA)</summary>
    //public const double NeutronMass = 1.674927212e-27;

    ///// <summary>Neutron Mass Energy Equivalent: [J] (2007 CODATA)</summary>
    //public const double NeutronMassEnegryEquivalent = 1.505349506e-10;

    ///// <summary>Neutron Molar Mass: [kg mol^-1] (2007 CODATA)</summary>
    //public const double NeutronMolarMass = 1.00866491597e-3;

    ///// <summary>Neuron Compton Wavelength: [m] (2007 CODATA)</summary>
    //public const double NeutronComptonWavelength = 1.3195908951e-1;

    ///// <summary>Neutron Magnetic Moment: [J T^-1] (2007 CODATA)</summary>
    //public const double NeutronMagneticMoment = -0.96623641e-26;

    ///// <summary>Neutron G-Factor: [1] (2007 CODATA)</summary>
    //public const double NeutronGFactor = -3.82608545;

    ///// <summary>Neutron Gyro-Magnetic Ratio: [s^-1 T^-1] (2007 CODATA)</summary>
    //public const double NeutronGyromagneticRatio = 1.83247185e8;

    ///// <summary>Deuteron Mass: [kg] (2007 CODATA)</summary>
    //public const double DeuteronMass = 3.34358320e-27;

    ///// <summary>Deuteron Mass Energy Equivalent: [J] (2007 CODATA)</summary>
    //public const double DeuteronMassEnegryEquivalent = 3.00506272e-10;

    ///// <summary>Deuteron Molar Mass: [kg mol^-1] (2007 CODATA)</summary>
    //public const double DeuteronMolarMass = 2.013553212725e-3;

    ///// <summary>Deuteron Magnetic Moment: [J T^-1] (2007 CODATA)</summary>
    //public const double DeuteronMagneticMoment = 0.433073465e-26;

    ///// <summary>Helion Mass: [kg] (2007 CODATA)</summary>
    //public const double HelionMass = 5.00641192e-27;

    ///// <summary>Helion Mass Energy Equivalent: [J] (2007 CODATA)</summary>
    //public const double HelionMassEnegryEquivalent = 4.49953864e-10;

    ///// <summary>Helion Molar Mass: [kg mol^-1] (2007 CODATA)</summary>
    //public const double HelionMolarMass = 3.0149322473e-3;

    ///// <summary>Avogadro constant: [mol^-1] (2010 CODATA)</summary>
    //public const double Avogadro = 6.0221412927e23;

    #endregion

    #endregion
}
