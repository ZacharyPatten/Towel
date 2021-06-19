using System;
using Towel;
using Towel.Mathematics;
using Towel.Measurements;
using static Towel.Measurements.MeasurementsSyntax; // allows measurement syntax
using Speedf = Towel.Measurements.Speed<float>;

namespace Measurements
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("You are runnning the Measurements example.");
			Console.WriteLine("==========================================");
			Console.WriteLine();

			#region Static Unit Conversion Methods
			{
				Console.WriteLine("  Static Conversion Methods-------------------------------");
				Console.WriteLine();

				var a = Measurement.Convert(7d,
					from: Radians,
					to: Degrees);

				Console.WriteLine("    var a = Measurement.Convert(7d,");
				Console.WriteLine("        from: Radians,");
				Console.WriteLine("        to:   Degrees)");
				Console.WriteLine();

				var b = Measurement.Convert(8d,
					from: Meters / Seconds,
					to: Miles / Hours);

				Console.WriteLine("    var b = Measurement.Convert(8d,");
				Console.WriteLine("        from: Meters / Seconds,");
				Console.WriteLine("        to:   Miles / Hours)");
				Console.WriteLine();

				var c = Measurement.Convert(9d,
					from: Kilograms * Meters / Seconds / Seconds,
					to: Grams * Miles / Hours / Hours);

				Console.WriteLine("    var b = Measurement.Convert(9d,");
				Console.WriteLine("        from: Kilograms * Meters / Seconds / Seconds,");
				Console.WriteLine("        to:   Grams * Miles / Hours / Hours)");
				Console.WriteLine();

				Console.WriteLine($"    a: {a}");
				Console.WriteLine($"    b: {b}");
				Console.WriteLine($"    c: {c}");
				Console.WriteLine();
			}
			#endregion

			#region Specific Measurement Type Examples

			#region Angle

			Angle<double> angle1 = (90, Degrees);
			Angle<double> angle2 = (0.5, Revolutions);

			Console.WriteLine("  Angle--------------------------------------");
			Console.WriteLine();
			Console.WriteLine("    angle1 = " + angle1);
			Console.WriteLine("    angle2 = " + angle2);
			Console.WriteLine("    angle1 + angle2 = " + (angle1 + angle2));
			Console.WriteLine("    angle2 - angle1 = " + (angle2 - angle1));
			Console.WriteLine("    angle1 * 2 = " + (angle1 * 2));
			Console.WriteLine("    angle1 / 2 = " + (angle1 / 2));
			Console.WriteLine("    angle1 > angle2 = " + (angle1 > angle2));
			Console.WriteLine("    angle1 == angle2 = " + (angle1 == angle2));
			Console.WriteLine("    angle1 * 2 == angle2 = " + (angle1 * 2 == angle2));
			Console.WriteLine("    angle1 != angle2 = " + (angle1 != angle2));
			Console.WriteLine();

			#endregion

			#region Length

			Length<double> length1 = (1d, Meters);
			Length<double> length2 = (2d, Yards);

			Console.WriteLine("  Length--------------------------------------");
			Console.WriteLine();
			Console.WriteLine("    length1 = " + length1);
			Console.WriteLine("    length2 = " + length2);
			Console.WriteLine("    length1 + length2 = " + (length1 + length2));
			Console.WriteLine("    length2 - length1 = " + (length2 - length1));
			Console.WriteLine("    length1 * 2 = " + (length1 * 2));
			Console.WriteLine("    length1 / 2 = " + (length1 / 2));
			Console.WriteLine("    length1 > length2 = " + (length1 > length2));
			Console.WriteLine("    length1 == length2 = " + (length1 == length2));
			Console.WriteLine("    length1 * 2 == length2 = " + (length1 * 2 == length2));
			Console.WriteLine("    length1 != length2 = " + (length1 != length2));
			Console.WriteLine();

			//object result = angle1 + length1; // WILL NOT COMPILE (this is a good thing) :)

			#endregion

			#region Mass

			Mass<double> mass1 = (1d, Grams);
			Mass<double> mass2 = (2d, Kilograms);

			Console.WriteLine("  Mass--------------------------------------");
			Console.WriteLine();
			Console.WriteLine("    mass1 = " + mass1);
			Console.WriteLine("    mass2 = " + mass2);
			Console.WriteLine("    mass1 + mass2 = " + (mass1 + mass2));
			Console.WriteLine("    mass2 - mass1 = " + (mass2 - mass1));
			Console.WriteLine("    mass1 * 2 = " + (mass1 * 2));
			Console.WriteLine("    mass1 / 2 = " + (mass1 / 2));
			Console.WriteLine("    mass1 > mass2 = " + (mass1 > mass2));
			Console.WriteLine("    mass1 == mass2 = " + (mass1 == mass2));
			Console.WriteLine("    mass1 * 2 == mass2 = " + (mass1 * 2 == mass2));
			Console.WriteLine("    mass1 != mass2 = " + (mass1 != mass2));
			Console.WriteLine();

			#endregion

			#region Time

			Time<double> time1 = (1d, Seconds);
			Time<double> time2 = (2d, Minutes);

			Console.WriteLine("  Time--------------------------------------");
			Console.WriteLine();
			Console.WriteLine("    time1 = " + time1);
			Console.WriteLine("    time2 = " + time2);
			Console.WriteLine("    time1 + time2 = " + (time1 + time2));
			Console.WriteLine("    time2 - time1 = " + (time2 - time1));
			Console.WriteLine("    time1 * 2 = " + (time1 * 2));
			Console.WriteLine("    time1 / 2 = " + (time1 / 2));
			Console.WriteLine("    time1 > time2 = " + (time1 > time2));
			Console.WriteLine("    time1 == time2 = " + (time1 == time2));
			Console.WriteLine("    time1 * 2 == time2 = " + (time1 * 2 == time2));
			Console.WriteLine("    time1 != time2 = " + (time1 != time2));
			Console.WriteLine();

			#endregion

			#region Area

			Area<double> area1 = (1d, Meters * Meters);
			Area<double> area2 = (2d, Yards * Yards);

			Console.WriteLine("  Area--------------------------------------");
			Console.WriteLine();
			Console.WriteLine("    area1 = " + area1);
			Console.WriteLine("    area2 = " + area2);
			Console.WriteLine("    area1 + area2 = " + (area1 + area2));
			Console.WriteLine("    area2 - area1 = " + (area2 - area1));
			Console.WriteLine("    area1 * 2 = " + (area1 * 2));
			Console.WriteLine("    area1 / 2 = " + (area1 / 2));
			Console.WriteLine("    area1 > area2 = " + (area1 > area2));
			Console.WriteLine("    area1 == area2 = " + (area1 == area2));
			Console.WriteLine("    area1 * 2 == area2 = " + (area1 * 2 == area2));
			Console.WriteLine("    area1 != area2 = " + (area1 != area2));
			Console.WriteLine();

			#endregion

			#region Volume

			Volume<double> volume1 = (1d, Meters * Meters * Meters);
			Volume<double> volume2 = (2d, Yards * Yards * Yards);

			Console.WriteLine("  Volume--------------------------------------");
			Console.WriteLine();
			Console.WriteLine("    volume1 = " + volume1);
			Console.WriteLine("    volume2 = " + volume2);
			Console.WriteLine("    volume1 + volume2 = " + (volume1 + volume2));
			Console.WriteLine("    volume2 - volume1 = " + (volume2 - volume1));
			Console.WriteLine("    volume1 * 2 = " + (volume1 * 2));
			Console.WriteLine("    volume1 / 2 = " + (volume1 / 2));
			Console.WriteLine("    volume1 > volume2 = " + (volume1 > volume2));
			Console.WriteLine("    volume1 == volume2 = " + (volume1 == volume2));
			Console.WriteLine("    volume1 * 2 == volume2 = " + (volume1 * 2 == volume2));
			Console.WriteLine("    volume1 != volume2 = " + (volume1 != volume2));
			Area<double> area3 = volume1 / length1;
			Console.WriteLine("    volume1 / length1 = " + area3);
			Console.WriteLine();

			#endregion

			#region Speed

			Speed<double> speed1 = (1d, Meters / Seconds);
			Speed<double> speed2 = (2d, Inches / Milliseconds);

			Console.WriteLine("  Speed--------------------------------------");
			Console.WriteLine();
			Console.WriteLine("    speed1 = " + speed1);
			Console.WriteLine("    speed2 = " + speed2);
			Console.WriteLine("    speed1 + speed2 = " + (speed1 + speed2));
			Console.WriteLine("    speed2 - speed1 = " + (speed2 - speed1));
			Console.WriteLine("    speed1 * 2 = " + (speed1 * 2));
			Console.WriteLine("    speed1 / 2 = " + (speed1 / 2));
			Console.WriteLine("    speed1 > speed2 = " + (speed1 > speed2));
			Console.WriteLine("    speed1 == speed2 = " + (speed1 == speed2));
			Console.WriteLine("    speed1 * 2 == speed2 = " + (speed1 * 2 == speed2));
			Console.WriteLine("    speed1 != speed2 = " + (speed1 != speed2));
			Console.WriteLine("    speed1 * time2 = " + (speed1 * time2));
			Speed<double> speed3 = (6d, Knots);
			Console.WriteLine("    speed3 = " + speed3);
			Console.WriteLine("    speed1 + speed3 = " + (speed1 + speed3));
			Console.WriteLine();

			#endregion

			#region Acceleration

			Acceleration<double> acceleration1 = (5d, Meters / Seconds / Seconds);
			Acceleration<double> acceleration2 = (4d, Inches / Milliseconds / Milliseconds);

			Console.WriteLine("  Acceleration--------------------------------------");
			Console.WriteLine();
			Console.WriteLine("    acceleration1 = " + acceleration1);
			Console.WriteLine("    acceleration2 = " + acceleration2);
			Console.WriteLine("    acceleration1 + acceleration2 = " + (acceleration1 + acceleration2));
			Console.WriteLine("    acceleration2 - acceleration1 = " + (acceleration2 - acceleration1));
			Console.WriteLine("    acceleration1 * 2 = " + (acceleration1 * 2));
			Console.WriteLine("    acceleration1 / 2 = " + (acceleration1 / 2));
			Console.WriteLine("    acceleration1 > acceleration2 = " + (acceleration1 > acceleration2));
			Console.WriteLine("    acceleration1 == acceleration2 = " + (acceleration1 == acceleration2));
			Console.WriteLine("    acceleration1 * 2 == acceleration2 = " + (acceleration1 * 2 == acceleration2));
			Console.WriteLine("    acceleration1 != acceleration2 = " + (acceleration1 != acceleration2));
			Console.WriteLine("    acceleration1 * time2 = " + (acceleration1 * time2));
			Console.WriteLine();

			#endregion

			#region Force

			Force<double> force1 = (1d, Kilograms * Meters / Seconds / Seconds);
			Force<double> force2 = (2d, Newtons);

			Console.WriteLine("  Force--------------------------------------");
			Console.WriteLine();
			Console.WriteLine("    force1 = " + force1);
			Console.WriteLine("    force2 = " + force2);
			Console.WriteLine("    force1 + force2 = " + (force1 + force2));
			Console.WriteLine("    force2 - force1 = " + (force2 - force1));
			Console.WriteLine("    force1 * 2 = " + (force1 * 2));
			Console.WriteLine("    force1 / 2 = " + (force1 / 2));
			Console.WriteLine("    force1 > force2 = " + (force1 > force2));
			Console.WriteLine("    force1 == force2 = " + (force1 == force2));
			Console.WriteLine("    force1 * 2 == force2 = " + (force1 * 2 == force2));
			Console.WriteLine("    force1 != force2 = " + (force1 != force2));
			Console.WriteLine();

			#endregion

			#region Electric Current

			ElectricCurrent<double> electricCurrent1 = (5d, Coulombs / Seconds);
			ElectricCurrent<double> electricCurrent2 = (4d, Amperes);

			Console.WriteLine("  ElectricCurrent--------------------------------------");
			Console.WriteLine();
			Console.WriteLine("    electricCurrent1 = " + electricCurrent1);
			Console.WriteLine("    electricCurrent2 = " + electricCurrent2);
			Console.WriteLine("    electricCurrent1 + electricCurrent2 = " + (electricCurrent1 + electricCurrent2));
			Console.WriteLine("    acceleration2 - electricCurrent1 = " + (electricCurrent2 - electricCurrent1));
			Console.WriteLine("    electricCurrent1 * 2 = " + (electricCurrent1 * 2));
			Console.WriteLine("    electricCurrent1 / 2 = " + (electricCurrent1 / 2));
			Console.WriteLine("    electricCurrent1 > electricCurrent2 = " + (electricCurrent1 > electricCurrent2));
			Console.WriteLine("    electricCurrent1 == electricCurrent2 = " + (electricCurrent1 == electricCurrent2));
			Console.WriteLine("    electricCurrent1 * 2 == electricCurrent2 = " + (electricCurrent1 * 2 == electricCurrent2));
			Console.WriteLine("    electricCurrent1 != electricCurrent2 = " + (electricCurrent1 != electricCurrent2));
			Console.WriteLine();

			#endregion

			#region AngularSpeed

			AngularSpeed<double> angularSpeed1 = (10d, Radians / Seconds);
			AngularSpeed<double> angularSpeed2 = (2200d, Degrees / Milliseconds);

			Console.WriteLine("  AngularSpeed--------------------------------------");
			Console.WriteLine();
			Console.WriteLine("    angularSpeed1 = " + angularSpeed1);
			Console.WriteLine("    angularSpeed2 = " + angularSpeed2);
			Console.WriteLine("    angularSpeed1 + angularSpeed2 = " + (angularSpeed1 + angularSpeed2));
			Console.WriteLine("    angularSpeed2 - angularSpeed1 = " + (angularSpeed2 - angularSpeed1));
			Console.WriteLine("    angularSpeed1 * 2 = " + (angularSpeed1 * 2));
			Console.WriteLine("    angularSpeed1 / 2 = " + (angularSpeed1 / 2));
			Console.WriteLine("    angularSpeed1 > angularSpeed2 = " + (angularSpeed1 > angularSpeed2));
			Console.WriteLine("    angularSpeed1 == angularSpeed2 = " + (angularSpeed1 == angularSpeed2));
			Console.WriteLine("    angularSpeed1 * 2 == angularSpeed2 = " + (angularSpeed1 * 2 == angularSpeed2));
			Console.WriteLine("    angularSpeed1 != angularSpeed2 = " + (angularSpeed1 != angularSpeed2));
			Console.WriteLine("    angularSpeed1 * time2 = " + (angularSpeed1 * time2));
			Console.WriteLine();

			#endregion

			#region AngularAcceleration

			AngularAcceleration<double> angularAcceleration1 = (5000d, Radians / Seconds / Seconds);
			AngularAcceleration<double> angularAcceleration2 = (.4d, Degrees / Milliseconds / Milliseconds);

			Console.WriteLine("  AngularAcceleration--------------------------------------");
			Console.WriteLine();
			Console.WriteLine("    angularAcceleration1 = " + angularAcceleration1);
			Console.WriteLine("    angularAcceleration2 = " + angularAcceleration2);
			Console.WriteLine("    angularAcceleration1 + angularAcceleration2 = " + (angularAcceleration1 + angularAcceleration2));
			Console.WriteLine("    angularAcceleration2 - angularAcceleration1 = " + (angularAcceleration2 - angularAcceleration1));
			Console.WriteLine("    angularAcceleration1 * 2 = " + (angularAcceleration1 * 2));
			Console.WriteLine("    angularAcceleration1 / 2 = " + (angularAcceleration1 / 2));
			Console.WriteLine("    angularAcceleration1 > angularAcceleration2 = " + (angularAcceleration1 > angularAcceleration2));
			Console.WriteLine("    angularAcceleration1 == angularAcceleration2 = " + (angularAcceleration1 == angularAcceleration2));
			Console.WriteLine("    angularAcceleration1 * 2 == angularAcceleration2 = " + (angularAcceleration1 * 2 == angularAcceleration2));
			Console.WriteLine("    angularAcceleration1 != angularAcceleration2 = " + (angularAcceleration1 != angularAcceleration2));
			Console.WriteLine("    angularAcceleration1 * time2 = " + (angularAcceleration1 * time2));
			Console.WriteLine();

			#endregion

			#region Desnity

			Density<double> density1 = (5d, Kilograms / Meters / Meters / Meters);
			Density<double> density2 = (2000d, Grams / Meters / Meters / Meters);

			Console.WriteLine("  Density--------------------------------------");
			Console.WriteLine();
			Console.WriteLine("    density1 = " + density1);
			Console.WriteLine("    density2 = " + density2);
			Console.WriteLine("    density1 + density2 = " + (density1 + density2));
			Console.WriteLine("    density2 - density1 = " + (density2 - density1));
			Console.WriteLine("    density1 * 2 = " + (density1 * 2));
			Console.WriteLine("    density1 / 2 = " + (density1 / 2));
			Console.WriteLine("    density1 > density2 = " + (density1 > density2));
			Console.WriteLine("    density1 == density2 = " + (density1 == density2));
			Console.WriteLine("    density1 * 2 == density2 = " + (density1 * 2 == density2));
			Console.WriteLine("    density1 != density2 = " + (density1 != density2));
			Console.WriteLine();

			#endregion

			#endregion

			#region Syntax Sugar Example (removing the generic type via alias)

			// If you hate seeing the "<float>" or "<double>" you can add syntax 
			// sugar to your files with an alias in C#:
			//
			// using Speedf = Towel.Measurements.Speed<float>;

			Speedf speedf = (1f, Meters / Seconds);

			#endregion

			#region Vectors Examples

			// You can use measurements inside Vectors in Towel.

			Vector<Speed<float>> velocity1 = new(
				(1f, Meters / Seconds),
				(2f, Meters / Seconds),
				(3f, Meters / Seconds));

			Vector<Speedf> velocity2 = new(
				(.1f, Centimeters / Seconds),
				(.2f, Centimeters / Seconds),
				(.3f, Centimeters / Seconds));

			Vector<Speed<float>> velocity3 = velocity1 + velocity2;

			#endregion

			#region Parsing
			{
				Console.WriteLine("  Parsing-------------------------------");
				Console.WriteLine();

				bool aSuccess = Angle<double>.TryParse(angle1.ToString(), out var a);
				Console.WriteLine($"    Angle<double>.TryParse({angle1}, out var {nameof(a)})");

				bool bSuccess = Length<double>.TryParse(length1.ToString(), out var b);
				Console.WriteLine($"    Length<double>.TryParse({length1}, out var {nameof(b)})");

				bool cSuccess = Density<double>.TryParse(density1.ToString(), out var c);
				Console.WriteLine($"    Density<double>.TryParse({density1}, out var {nameof(c)})");

				string speedString = "20.5 Meters / Seconds";
				bool dSuccess = Speed<float>.TryParse(speedString, out var d);
				Console.WriteLine($"    Speed<float>.TryParse({speedString}, out var {nameof(d)})");

				string forceString = ".1234 Kilograms * Meters / Seconds / Seconds";
				bool eSuccess = Force<decimal>.TryParse(forceString, out var e);
				Console.WriteLine($"    Force<decimal>.TryParse({forceString}, out var {nameof(e)})");

				string densityString = "12.344 Kilograms / Centimeters / Centimeters / Centimeters";
				bool fSuccess = Density<double>.TryParse(densityString, out var f);
				Console.WriteLine($"    Density<decimal>.TryParse({densityString}, out var {nameof(f)})");

				Console.WriteLine();
				Console.WriteLine($"    a: {aSuccess}, {a}");
				Console.WriteLine($"    b: {bSuccess}, {b}");
				Console.WriteLine($"    c: {cSuccess}, {c}");
				Console.WriteLine($"    d: {dSuccess}, {d}");
				Console.WriteLine($"    e: {eSuccess}, {e}");
				Console.WriteLine($"    f: {fSuccess}, {f}");
				Console.WriteLine();
			}
			#endregion

			Console.WriteLine("=================================================");
			Console.WriteLine("Example Complete...");
			Console.WriteLine();
			ConsoleHelper.PromptPressToContinue();
		}
	}
}
