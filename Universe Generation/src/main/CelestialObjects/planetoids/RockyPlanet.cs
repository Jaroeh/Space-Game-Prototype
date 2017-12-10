using System;
using System.Collections.Generic;
using Space_Explorer.main.CelestialObjects.stellar;
using Space_Explorer.main.CelestialObjects.universe;
using Space_Explorer.main.Resources;

namespace Space_Explorer.main.CelestialObjects.planetoids {
    public class RockyPlanet : Planetoid
    {
        public RockyPlanet(byte planetId, string planetName, string description, StarSystem parentStarSystem, Star parentStar, float auFromParent)
        {
            Universe.PlanetCount++;
            Id = planetId;
            Name = planetName;
            Description = description;
            ParentObject = parentStarSystem;
            AuFromParent = auFromParent;
            Moons = new List<Moon>();
            ChildRings = new List<Ring>();


            //Calculating Physical Properties
            Radius = GenerateRadius();
            ResourcesPresent = CalculateBodyResources();
            Volume = CalculateVolume(radius: Radius); //Calculating the volume based on the radius of the planet (assuming a spherical planet)
            AverageDensity = CalculateAverageDensity(resourcesPresent: ResourcesPresent); //Based on resources present
            Mass = CalculateMass(volume: Volume, density: AverageDensity); //Calculates and stores the mass of the planet in Kilograms. Based on the individual elemental densities and their percentage of the total volume of the planet
            Gravity = CalculateGravity(mass: Mass, radius: Radius); //Based on the mass and radius in meters of the planet
            GeologicActivityLevel = CalculateGeologicalActivityLevel();
            LandmassRatio = CalculateLandmassRatio();

            //Calculating Temperature and Radiation based properties
            SolarInsolation = CalculateSolarInsolation(auFromStar: auFromParent, parentStar: parentStar);
            BaseTemperature = CalculateBaseTemperature(solarInsolation: SolarInsolation);
            GreenhouseModifier = CalculateGreenhouseMultiplier();
            AverageSurfaceTemperature = CalculateInitialSurfaceTemperature(effectiveTemperature: BaseTemperature, greenhouseModifier: GreenhouseModifier);

            //Calculating Resource dispersal and state  //Todo: Clean this up!!! Please!!! Seems redundant to pass the ResourcesByState object to the method only to place the passed object right back into itself. (objects are pass by reference...)
            ResourcesByState = CalculateResourceStates(resourcesPresent: ResourcesPresent, surfaceTemperature: AverageSurfaceTemperature);
            Dictionary<byte, Resource> atmospherics = CalculateResourceAbundance(resourceList: ResourcesByState.GetAtmospherics());
            Dictionary<byte, Resource> liquids = CalculateResourceAbundance(resourceList: ResourcesByState.GetLiquids());
            Dictionary<byte, Resource> solids = CalculateResourceAbundance(resourceList: ResourcesByState.GetSolids());

            ResourcesByState = new ObjectResources(solids: solids, atmospherics: atmospherics, liquids: liquids);

            //Calculating Biological properties
            FloraBioDiversity = CalulateBioDiversity();
            FaunaBioDiversity = CalulateBioDiversity();

            //Calculating the atmospheric density based off of the resources present.
            if (ResourcesByState.GetAtmospherics() != null && ResourcesByState.GetAtmospherics().Count > 0)
            {
                AtmosphericDensity = CalculateAtmosphericDensity(atmosResourcesPresent: ResourcesByState.GetAtmospherics());
            }
            else
            {
                AtmosphericDensity = 0;
            }

            GenerateMoons();
        }

        private static byte CalculateGeologicalActivityLevel()
        {
            Random random = new Random(Seed: DateTime.Now.Millisecond);
            return (byte)random.Next(minValue: 0, maxValue: 100);
        }
    }
}