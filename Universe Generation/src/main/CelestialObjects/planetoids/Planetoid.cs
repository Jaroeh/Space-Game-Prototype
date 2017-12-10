using System;
using System.Collections.Generic;
using Space_Explorer.main.CelestialObjects.universe;
using Space_Explorer.main.Resources;
using static Space_Explorer.main.utils.AstroPhysicsUtils.ConversionsAndConstants;

namespace Space_Explorer.main.CelestialObjects.planetoids
{
    public class Planetoid : CelestialObject
    {
        public bool BreathableAtmosphere;
        public short GreenhouseModifier;
        public byte AtmosphericDensity;
        public byte GeologicActivityLevel;
        public byte LandmassRatio;
        public byte FloraBioDiversity;
        public byte FaunaBioDiversity;
        public List<Moon> Moons;
        public List<Ring> ChildRings;

        public double GenerateRadius()
        {
            int upperRadius, lowerRadius;

            if (GetType() == typeof(Asteroid))
            {
                upperRadius = 1000;
                lowerRadius = 1;
            }
            else if (GetType() == typeof(RockyPlanet))
            {
                upperRadius = 13000;
                lowerRadius = 500;
            }
            else if (GetType() == typeof(GasGiant))
            {
                upperRadius = 60000;
                lowerRadius = 13000;
            }
            else throw new ArgumentOutOfRangeException(paramName: GetType().ToString(), message: @"Calculating the radius of an unsupported object has been attempted.");

            Random rand = new Random(Seed: DateTime.Now.Millisecond);

            double radius = rand.Next(minValue: lowerRadius, maxValue: upperRadius);
            return radius;
        }

        public double GenerateRadius(int upperRadius, int lowerRadius)
        {
            Random rand = new Random(Seed: DateTime.Now.Millisecond);
            double radius = rand.Next(minValue: lowerRadius, maxValue: upperRadius);
            return radius;
        }

        private enum GreenhouseCategory
        {
            Low = 0,
            Medium = 1,
            High = 2
        }

        internal static short CalculateGreenhouseMultiplier()
        {
            Random rand = new Random(Seed: DateTime.Now.Millisecond);
            short greenhouseMultiplier;

            int categorySelector = rand.Next(minValue: 1, maxValue: 100);
            GreenhouseCategory greenhouseCategory;
            if (categorySelector < 25) greenhouseCategory = GreenhouseCategory.Low;
            else if (categorySelector < 75) greenhouseCategory = GreenhouseCategory.Medium;
            else if (categorySelector <= 100) greenhouseCategory = GreenhouseCategory.High;
            else throw new ArgumentOutOfRangeException();

            switch (greenhouseCategory)
            {
                case GreenhouseCategory.Low:
                    greenhouseMultiplier = (short)rand.Next(minValue: 0, maxValue: 49);
                    break;
                case GreenhouseCategory.Medium:
                    greenhouseMultiplier = (short)rand.Next(minValue: 50, maxValue: 199);
                    break;
                case GreenhouseCategory.High:
                    greenhouseMultiplier = (short)rand.Next(minValue: 200, maxValue: 300);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return greenhouseMultiplier;
        }

        public static float CalculateInitialSurfaceTemperature(float effectiveTemperature, short greenhouseModifier = -1)
        {
            double kelvins;
            Random random = new Random(Seed: DateTime.Now.Millisecond);
            Random rand = new Random(Seed: DateTime.Now.Second);
            byte categorySelection = (byte)random.Next(minValue: 0, maxValue: 100);
            if (greenhouseModifier != -1)
            {
                if (categorySelection < 20) greenhouseModifier = (short)rand.Next(minValue: 0, maxValue: 49);
                else if (categorySelection < 80) greenhouseModifier = (short)rand.Next(minValue: 50, maxValue: 199);
                else greenhouseModifier = (short)rand.Next(minValue: 200, maxValue: 300);
            }

            if (greenhouseModifier == 0)
            {
                kelvins = effectiveTemperature;
            }
            else
            {
                kelvins = Math.Pow(x: greenhouseModifier, y: .25) * effectiveTemperature;
            }

            return (float)kelvins;
        } //Calculates the Temperature at the planets surface based on the determined greenhouse effect

        internal static double CalculateGravity(double mass, double radius)
        {
            return GravitationalConstant * (mass / Math.Pow(x: (radius * 1000), y: 2));
        }

        public static double CalculateVolume(double radius)
        {
            double volume = 4F * Math.PI * (Math.Pow(x: radius, y: 3) / 3F);
            return volume;
        }

        public static byte CalculateLandmassRatio()
        {
            Random random = new Random(Seed: DateTime.Now.Millisecond);
            return (byte)(random.Next(minValue: 0, maxValue: 100) / 100);
        }

        public static byte CalulateBioDiversity()
        {
            Random random = new Random(Seed: DateTime.Now.Millisecond);
            return (byte)random.Next(minValue: 1, maxValue: 3);
        }

        public static byte CalculateAtmosphericDensity(Dictionary<byte, Resource> atmosResourcesPresent)
        {
            Random random = new Random(Seed: DateTime.Now.Millisecond);
            return (byte)random.Next(minValue: 1, maxValue: 100);
        }

        internal void GenerateMoons()
        {
            Random randMoonCreation = new Random(Seed: DateTime.Now.Millisecond);

            if (randMoonCreation.Next(minValue: 0, maxValue: 2) != 1) return;
            Moons.Add(item: new Moon(moonId: 'a', moonName: String.Concat(str0: Name, str1: "-a"), moonDescription: "Placeholder", parentPlanet: this));
            Universe.MoonACount++;

            if (randMoonCreation.Next(minValue: 0, maxValue: 5) != 2) return;
            Moons.Add(item: new Moon(moonId: 'b', moonName: String.Concat(str0: Name, str1: "-b"), moonDescription: "Placeholder", parentPlanet: this));
            Universe.MoonBCount++;

            if (randMoonCreation.Next(minValue: 0, maxValue: 11) != 5) return;
            Moons.Add(item: new Moon(moonId: 'c', moonName: String.Concat(str0: Name, str1: "-c"), moonDescription: "Placeholder", parentPlanet: this));
            Universe.MoonCCount++;
        }
    }
}
