using System;
using System.Collections.Generic;
using Space_Explorer.main.CelestialObjects.universe;
using Space_Explorer.main.Resources;

namespace Space_Explorer.main.CelestialObjects.planetoids {
    public class Moon : Planetoid
    {
        public float KilometersFromParent;

        public Moon(char moonId, string moonName, string moonDescription, Planetoid parentPlanet, float kilometersFromParent = -1)
        {
            Universe.MoonCount++;
            Id = moonId;
            Name = moonName;
            Description = moonDescription;
            Random random = new Random(Seed: DateTime.Now.Millisecond);
            MoonCategory moonCategory = PickMoonCategory();
            Radius = GenerateRadius(category: moonCategory, parentPlanet: parentPlanet);
            KilometersFromParent = kilometersFromParent == -1 ? DetermineDistanceFromParentBody(planetRadius: parentPlanet.Radius, moonRadius: Radius) : kilometersFromParent;
            AverageDensity = parentPlanet.AverageDensity;
            BaseTemperature = parentPlanet.BaseTemperature;
            ResourcesPresent = parentPlanet.ResourcesPresent;
            SolarInsolation = parentPlanet.SolarInsolation;
            AxialTilt = 0;
            GeologicActivityLevel = 0;
            //RotationalPeriod = ;
            Volume = CalculateVolume(radius: Radius);
            Mass = CalculateMass(volume: Volume, density: AverageDensity);
            Gravity = CalculateGravity(mass: Mass, radius: Radius);

            switch (moonCategory)
            {
                case MoonCategory.Small:
                    CalculateBarrenMoonProperties(moon: this);
                    break;

                case MoonCategory.Medium:
                    if (random.Next(minValue: 0, maxValue: 100) > 65) CalculateAtmosphericMoonProperties(moon: this);
                    else CalculateBarrenMoonProperties(moon: this);
                    break;

                case MoonCategory.Large:
                    if (random.Next(minValue: 0, maxValue: 100) > 10) CalculateAtmosphericMoonProperties(moon: this);
                    else CalculateBarrenMoonProperties(moon: this);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            AverageSurfaceTemperature = CalculateInitialSurfaceTemperature(effectiveTemperature: BaseTemperature, greenhouseModifier: GreenhouseModifier);

            ResourcesByState = CalculateResourceStates(resourcesPresent: ResourcesPresent, surfaceTemperature: AverageSurfaceTemperature);

            if (AtmosphericDensity > 0)
            {
                Dictionary<byte, Resource> atmospherics = CalculateResourceAbundance(resourceList: ResourcesByState.GetAtmospherics());
                ResourcesByState = new ObjectResources(solids: ResourcesByState.GetSolids(), atmospherics: atmospherics, liquids: ResourcesByState.GetLiquids());
                CalculateAtmosphericDensity(atmosResourcesPresent: ResourcesByState.GetAtmospherics());
            }
            else
            {
                Dictionary<byte, Resource> atmospherics = null;
                ResourcesByState = new ObjectResources(solids: ResourcesByState.GetSolids(), atmospherics: atmospherics, liquids: ResourcesByState.GetLiquids());
            }

            Dictionary<byte, Resource> liquids = CalculateResourceAbundance(resourceList: ResourcesByState.GetLiquids());
            Dictionary<byte, Resource> solids = CalculateResourceAbundance(resourceList: ResourcesByState.GetSolids());

            ResourcesByState = new ObjectResources(solids: solids, atmospherics: ResourcesByState.GetAtmospherics(), liquids: liquids);
        }

        private static MoonCategory PickMoonCategory()
        {
            Random rand = new Random(Seed: DateTime.Now.Millisecond);
            MoonCategory category;

            int chance = rand.Next(minValue: 1, maxValue: 100);

            if (chance < 50) category = MoonCategory.Small;
            else if (chance < 85) category = MoonCategory.Medium;
            else if (chance < 100) category = MoonCategory.Large;
            else throw new ArgumentOutOfRangeException();

            return category;
        }

        private double GenerateRadius(MoonCategory category, Planetoid parentPlanet)
        {
            Random random = new Random(Seed: DateTime.Now.Millisecond);
            double radius;
            switch (category)
            {
                case MoonCategory.Small:
                    radius = parentPlanet.Radius * (random.Next(minValue: 1, maxValue: 20) / 10000F);
                    break;
                case MoonCategory.Medium:
                    radius = parentPlanet.Radius * (random.Next(minValue: 21, maxValue: 2500) / 10000F);
                    break;
                case MoonCategory.Large:
                    radius = parentPlanet.Radius * (random.Next(minValue: 2501, maxValue: 7500) / 10000F);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(paramName: nameof(category), actualValue: category, message: null);
            }

            return radius;
        }

        private static float DetermineDistanceFromParentBody(double planetRadius, double moonRadius)
        {
            Random rand = new Random(Seed: DateTime.Now.Millisecond);

            int minimumDistance = (int)(planetRadius + moonRadius + 50);
            int maximumDistance = (int)(planetRadius + moonRadius + 700000);

            float moonOrbitDistance = rand.Next(minValue: minimumDistance, maxValue: maximumDistance);

            return moonOrbitDistance;
        }

        private static void CalculateAtmosphericMoonProperties(Moon moon)
        {
            moon.GreenhouseModifier = CalculateGreenhouseMultiplier();
            moon.AtmosphericDensity = CalculateAtmosphericDensity(atmosResourcesPresent: moon.ResourcesByState.GetAtmospherics());
            moon.BreathableAtmosphere = true;
            moon.FaunaBioDiversity = CalulateBioDiversity();
            moon.FloraBioDiversity = CalulateBioDiversity();
            moon.LandmassRatio = CalculateLandmassRatio();
        }

        private static void CalculateBarrenMoonProperties(Moon moon)
        {
            moon.AtmosphericDensity = 0;
            moon.BreathableAtmosphere = false;
            moon.FaunaBioDiversity = 0;
            moon.FloraBioDiversity = 0;
            moon.LandmassRatio = 1;
            moon.GreenhouseModifier = 0;
        }
    }
}