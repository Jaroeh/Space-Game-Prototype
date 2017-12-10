using System;
using System.Collections.Generic;
using Space_Explorer.main.CelestialObjects.universe;
using Space_Explorer.main.Resources;

namespace Space_Explorer.main.CelestialObjects.planetoids
{
    public class Ring : CelestialObject
    {
        public byte Density;
        public List<Resource> SolidResources = new List<Resource>();
        public float KilometersFromHostPlanet;
        public float RingWidthInKilometers;

        public Ring(CelestialObject parentPlanetoid)
        {
            Universe.RingCount++;
            Density = CalculateRingDensity();
            SolarInsolation = parentPlanetoid.SolarInsolation;
            BaseTemperature = CalculateBaseTemperature(solarInsolation: SolarInsolation);
            ResourcesPresent = CalculateBodyResources();
            ResourcesByState = CalculateResourceStates(resourcesPresent: ResourcesPresent, surfaceTemperature: BaseTemperature);

            //Rings do not have atmospheres or liquids. Liquids would either boil off into space or freeze and atmospherics would be blown away by the stellar wind due to insufficient gravity
            ResourcesByState = new ObjectResources(solids: ResourcesByState.GetSolids(), atmospherics: null, liquids: null);
        }

        private byte CalculateRingDensity()
        {
            Random rand = new Random(Seed: DateTime.Now.Millisecond);
            byte density = (byte)rand.Next(minValue: 0, maxValue: 255);
            return density;
        }
    }
}
