using System;
using Space_Explorer.main.CelestialObjects.universe;
using static Space_Explorer.main.utils.ObjectUtils;
using static Space_Explorer.main.utils.AstroPhysicsUtils.ConversionsAndConstants;


namespace Space_Explorer.main.CelestialObjects.stellar
{
    public class Star
    {
        public ushort Id;
        public string Name;
        public string Description;
        private const double SolarTemperature = 5800;   //In kelvins
        private const float SolarMagnitude = 4.74F;
        private const double SolarLuminosity = 3.828e26;
        private const double SolarMass = 2e30;
        private const int SolarRadius = 695700; //In Kilometers

        public readonly LuminosityClassification LuminosityClass;   //Selected by psuedo-random
        public SpectralClassificationEnum SpectralClass;            //Selected by psuedo-random
        public sbyte Magnitude;                                     //Randomly selected based on Luminosity Type
        public int Temperature;                                     //Randomly selected based on Luminosity Type    : Stored in Kelvins
        public readonly double SolarFlux;                           //W/m^2
        public readonly double Luminosity;                          //Calculated based on magnitude                 : Stored in Watts
        public double Mass;                                         //Calculated based off of temperature           : Stored in Kilograms
        public readonly double Radius;                              //Calculated based on Luminosity and Temperature : Stored in Kilometers
        public float HabitableZoneInnerRadius;                      //Inner Radius = sqrt(bolometric Luminosity/1.1)
        public float HabitableZoneOuterRadius;                      //Outer Radius = sqrt(bolometric Luminosity/.53)

        public Star()
        {
            Universe.StarCount++;
            this.Name = "Temp";  //TODO: Add naming convention based on stars data: spectralClass-Luminosity-RandomGenNumber (check name against list to ensure no duplicates)
            //DO NOT CHANGE ORDER OF THESE!!! EACH ONE MAY BE DEPENDENT ON A VALUE CALCULATED BY THE ONE BEFORE IT!!!
            LuminosityClass = SelectPsuedoRandomLuminosityClass();  //Luminosity Class
            SelectRandomMagnitudeAndTemperature();                  //Spectral Class, Magnitude, and Temperature
            Luminosity = CalculateBolometricLuminosity();           //Luminosity in Watts
            SolarFlux = CalculateSolarFlux();
            Radius = CalculateRadius();                             //Radius in Kilometers
            Mass = CalculateClassVStarMass();                       //Mass in Kilograms
            float[] habZone = CalculateHabitableZone();             //Inner and outer habitable zones in AU
            HabitableZoneInnerRadius = habZone[index0: 0];
            HabitableZoneOuterRadius = habZone[index0: 1];
        }

        private LuminosityClassification SelectPsuedoRandomLuminosityClass()
        {
            //****DISABLING UNITL RELIABLE METHOD FOR CALCULATING THE MASS OF NON-MAIN-SEQUENCE STARS IS FOUND****//
            /*
            Random rand = new Random();
            short lumClass = (short) rand.Next(1, 1000);
                
            if (lumClass <= 15)     return LuminosityClassification.I;      //Supergiant
            if (lumClass <= 55)     return LuminosityClassification.III;    //Giant
            if (lumClass <= 9500)   return LuminosityClassification.V;      //Main Sequence
            else                    return LuminosityClassification.D;      //White Dwarf
            */
            return LuminosityClassification.V;
        }

        private void SelectRandomMagnitudeAndTemperature()
        {
            try
            {
                Random rand = new Random();
                switch (LuminosityClass)
                {
                    case LuminosityClassification.I:
                        SpectralClass = (SpectralClassificationEnum)rand.Next(minValue: (int)StarClassifications.Supergiant.MaxSpectralClass, maxValue: (int)StarClassifications.Supergiant.MinSpectralClass);
                        Magnitude = (sbyte)rand.Next(minValue: StarClassifications.Supergiant.MinMagnitude, maxValue: StarClassifications.Supergiant.MaxMagnitude);
                        break;
                    case LuminosityClassification.II:
                        break;
                    case LuminosityClassification.III:
                        SpectralClass = (SpectralClassificationEnum)rand.Next(minValue: (int)StarClassifications.Giant.MaxSpectralClass, maxValue: (int)StarClassifications.Giant.MinSpectralClass);
                        Magnitude = (sbyte)rand.Next(minValue: StarClassifications.Giant.MinMagnitude, maxValue: StarClassifications.Giant.MaxMagnitude);
                        break;
                    case LuminosityClassification.IV:
                        break;
                    case LuminosityClassification.V:
                        SpectralClass = (SpectralClassificationEnum)rand.Next(minValue: (int)StarClassifications.MainSequence.MaxSpectralClass, maxValue: (int)StarClassifications.MainSequence.MinSpectralClass);
                        Temperature = CalculateTemperatureFromClass();
                        Magnitude = (sbyte)rand.Next(minValue: StarClassifications.MainSequence.MinMagnitude, maxValue: StarClassifications.MainSequence.MaxMagnitude);
                        break;
                    case LuminosityClassification.D:
                        SpectralClass = (SpectralClassificationEnum)rand.Next(minValue: (int)StarClassifications.WhiteDwarf.MaxSpectralClass, maxValue: (int)StarClassifications.WhiteDwarf.MinSpectralClass);
                        Magnitude = (sbyte)rand.Next(minValue: StarClassifications.WhiteDwarf.MinMagnitude, maxValue: StarClassifications.WhiteDwarf.MaxMagnitude);
                        break;
                }
            }
            catch (Exception ex)
            {
                Exception baseException = ex.GetBaseException();
                DisplayException(source: baseException.Source, message: baseException.Message, functionName: baseException.TargetSite.Name, stackTrace: baseException.StackTrace);
                throw;
            }
        }

        private double CalculateBolometricLuminosity()        //Takes bolometric magnitude and outputs luminosity in Watts
        {
            try
            {
                return Math.Pow(x: 10, y: (Magnitude - 4.72) / -2.5) * SolarLuminosity;   //Units in Watts
            }

            catch (Exception ex)
            {
                Exception baseException = ex.GetBaseException();
                DisplayException(source: baseException.Source, message: baseException.Message, functionName: baseException.TargetSite.Name, stackTrace: baseException.StackTrace);
                throw;
            }
        }

        private float[] CalculateHabitableZone()           //Outputs inner and outer hab zone radius in AU
        {
            try
            {
                float[] habitableZone = new float[2];
                habitableZone[index0: 0] = (float)Math.Sqrt(d: (Luminosity / SolarLuminosity) / 1.1);     //Inner Edge of Habitable Zone
                habitableZone[index0: 1] = (float)Math.Sqrt(d: (Luminosity / SolarLuminosity) / .53F);    //Outer Edge of Habitable Zone
                return habitableZone;
            }
            catch (Exception ex)
            {
                Exception baseException = ex.GetBaseException();
                DisplayException(source: baseException.Source, message: baseException.Message, functionName: baseException.TargetSite.Name, stackTrace: baseException.StackTrace);
                throw;
            }
        }

        private int CalculateTemperatureFromClass()       //Outputs the stars temperature in Kelvins based on the stars class
        {
            try
            {
                int temp = (int)Math.Round(a: 60002.44140875778 - 1309.4222928168224 * (byte)SpectralClass - 477.58468196623943 * Math.Pow(x: (byte)SpectralClass, y: 2) + 41.825541294412446 * Math.Pow(x: (byte)SpectralClass, y: 3) - 1.507554288118054 * Math.Pow(x: (byte)SpectralClass, y: 4) + 0.027813041798434066 * Math.Pow(x: (byte)SpectralClass, y: 5) - 0.00025913058544489915 * Math.Pow(x: (byte)SpectralClass, y: 6) + 9.6735340088598e-7 * Math.Pow(x: (byte)SpectralClass, y: 7));
                return temp;
            }
            catch (Exception ex)
            {
                Exception baseException = ex.GetBaseException();
                DisplayException(source: baseException.Source, message: baseException.Message, functionName: baseException.TargetSite.Name, stackTrace: baseException.StackTrace);
                throw;
            }
        }

        private double CalculateRadius()    //Outputs the stars radius in Kilometers
        {
            try
            {
                double radius = Math.Sqrt(d: Luminosity / (4 * Math.PI * StefanBoltzmannConstant * Math.Pow(x: Temperature, y: 4))); //Temperature in Kelvins : SolarRadius in Kilometers : Luminosity in watts
                return radius;
            }
            catch (Exception ex)
            {
                Exception baseException = ex.GetBaseException();
                DisplayException(source: baseException.Source, message: baseException.Message, functionName: baseException.TargetSite.Name, stackTrace: baseException.StackTrace);
                throw;
            }
        }

        private double CalculateClassVStarMass()              //Outputs the stars mass in Kilograms
        {
            try
            {
                double mass = (-0.4410064795543209 + 0.00006004975385848168 * Temperature + 4.653056719187e-8 * Math.Pow(x: Temperature, y: 2) - 2.19181807e-12 * Math.Pow(x: Temperature, y: 3) + 3.967e-17 * Math.Pow(x: Temperature, y: 4)) * SolarMass;
                return mass;
            }
            catch (Exception ex)
            {
                Exception baseException = ex.GetBaseException();
                DisplayException(source: baseException.Source, message: baseException.Message, functionName: baseException.TargetSite.Name, stackTrace: baseException.StackTrace);
                throw;
            }
        }

        private double CalculateSolarFlux()
        {
            return .98029235F * StefanBoltzmannConstant * Math.Pow(x: Temperature, y: 4);
        }

        public enum LuminosityClassification
        {
            I = 1,
            II = 2,
            III = 3,
            IV = 4,
            V = 5,
            D = 6
        }

        public enum SpectralClassificationEnum
        {
            O0 = 0,
            O1 = 1,
            O2 = 2,
            O3 = 3,
            O4 = 4,
            O5 = 5,
            O6 = 6,
            O7 = 7,
            O8 = 8,
            O9 = 9,
            B0 = 10,
            B1 = 11,
            B2 = 12,
            B3 = 13,
            B4 = 14,
            B5 = 15,
            B6 = 16,
            B7 = 17,
            B8 = 18,
            B9 = 19,
            A0 = 20,
            A1 = 21,
            A2 = 22,
            A3 = 23,
            A4 = 24,
            A5 = 25,
            A6 = 26,
            A7 = 27,
            A8 = 28,
            A9 = 29,
            F0 = 30,
            F1 = 31,
            F2 = 32,
            F3 = 33,
            F4 = 34,
            F5 = 35,
            F6 = 36,
            F7 = 37,
            F8 = 38,
            F9 = 39,
            G0 = 40,
            G1 = 41,
            G2 = 42,
            G3 = 43,
            G4 = 44,
            G5 = 45,
            G6 = 46,
            G7 = 47,
            G8 = 48,
            G9 = 49,
            K0 = 50,
            K1 = 51,
            K2 = 52,
            K3 = 53,
            K4 = 54,
            K5 = 55,
            K6 = 56,
            K7 = 57,
            K8 = 58,
            K9 = 59,
            M0 = 60,
            M1 = 61,
            M2 = 62,
            M3 = 63,
            M4 = 64,
            M5 = 65,
            M6 = 66,
            M7 = 67,
            M8 = 68,
            M9 = 69,
        }

        private class StarClassifications
        {
            //All Luminosities are as compared to the sun. The sun has a luminosity of 1.
            public static class Supergiant
            {
                public const string Classification = "I";
                public const SpectralClassificationEnum MinSpectralClass = SpectralClassificationEnum.M3;
                public const SpectralClassificationEnum MaxSpectralClass = SpectralClassificationEnum.O8;
                public const sbyte MinMagnitude = -8;   //Measured in Suns
                public const sbyte MaxMagnitude = -3;   //Measured in Suns
                public const float Abundance = .0015F;  //Chance of Occurence
            }

            public static class Giant
            {
                public const string Classification = "III";
                public const SpectralClassificationEnum MinSpectralClass = SpectralClassificationEnum.M5;
                public const SpectralClassificationEnum MaxSpectralClass = SpectralClassificationEnum.F4;
                public const sbyte MinMagnitude = -3;  //Measured in Suns
                public const sbyte MaxMagnitude = 2;   //Measured in Suns
                public const float Abundance = .004F;  //Chance of Occurence
            }

            public static class MainSequence
            {
                public const string Classification = "V";
                public const SpectralClassificationEnum MinSpectralClass = SpectralClassificationEnum.M8;
                public const SpectralClassificationEnum MaxSpectralClass = SpectralClassificationEnum.O3;
                public const sbyte MinMagnitude = -3;           //Measured in Suns
                public const sbyte MaxMagnitude = 16;           //Measured in Suns
                public const float Abundance = .9445F;          //Chance of Occurence
            }

            public static class WhiteDwarf
            {
                public const string Classification = "D";
                public const SpectralClassificationEnum MinSpectralClass = SpectralClassificationEnum.G5;
                public const SpectralClassificationEnum MaxSpectralClass = SpectralClassificationEnum.B1;
                public const sbyte MinMagnitude = 11;           //Measured in Suns
                public const sbyte MaxMagnitude = 16;           //Measured in Suns
                public const float Abundance = .05F;            //Chance of Occurence
            }
        }
    }
}
