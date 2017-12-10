using System;
using System.Collections.Generic;
using Space_Explorer.main.CelestialObjects.stellar;
using Space_Explorer.main.Resources;
using Space_Explorer.main.utils;
using static Space_Explorer.main.utils.AstroPhysicsUtils.ConversionsAndConstants;

//**** Default Units of Measurement ****//
//
//  Mass             :   Kilograms
//  Volume          :   Meters^2
//  Temperature     :   Kelvins
//  Distance        :   Kilometers
//  Dist From Star  :   Astronomical Units
//
//****                              ****//

namespace Space_Explorer.main.CelestialObjects
{
    public class CelestialObject
    {
        public ushort Id;
        public string Name;
        public string Description;
        public object ParentObject;
        public Dictionary<byte, Resource> ResourcesPresent;   //List of resources present
        public ObjectResources ResourcesByState = new ObjectResources();
        public double SolarInsolation;  //W/m^2
        public float BaseTemperature;   //Kelvins
        public float AverageSurfaceTemperature; //Kelvins
        public float AverageDensity;    //Grams/meter^3
        public double Radius;           //Kilometers
        public double Volume;           //Kilometers^3
        public double Mass;             //Stored in kilograms
        public double Gravity;          //Calculated by gravity = G * Mass / radius^2 : gravity-meters/second : G-Gravitational Constant : mass-kilograms : radius-meters
        public short RotationalPeriod;
        public byte AxialTilt;          //Degrees
        public float AuFromParent;

        public class ObjectResources {
            private readonly Dictionary<byte, Resource> Solids = new Dictionary<byte, Resource>();
            private readonly Dictionary<byte, Resource> Atmospherics = new Dictionary<byte, Resource>();
            private readonly Dictionary<byte, Resource> Liquids = new Dictionary<byte, Resource>();

            public ObjectResources() { }

            public ObjectResources(Dictionary<byte, Resource> solids, Dictionary<byte, Resource> atmospherics, Dictionary<byte, Resource> liquids) {
                Solids = solids;
                Atmospherics = atmospherics;
                Liquids = liquids;
            }

            public Dictionary<byte, Resource> GetSolids() {
                return Solids;
            }

            public Dictionary<byte, Resource> GetAtmospherics() {
                return Atmospherics;
            }

            public Dictionary<byte, Resource> GetLiquids() {
                return Liquids;
            }

            public void AddSolid(byte id, Resource resource) {
                Solids.Add(key: id, value: resource);
            }

            public void AddAtmospheric(byte id, Resource resource)
            {
                Atmospherics.Add(key: id, value: resource);
            }

            public void AddLiquid(byte id, Resource resource)
            {
                Liquids.Add(key: id, value: resource);
            }
        }

        internal static Dictionary<byte, Resource> CalculateBodyResources()
        {
            Dictionary<byte, Resource> resourceList = new Dictionary<byte, Resource>();
            Random random = new Random(Seed: DateTime.Now.Millisecond);

            foreach (KeyValuePair<byte, Resource> resource in Globals.Resources)
            {
                int randNum = random.Next(minValue: 0, maxValue: 100);
                if (randNum < resource.Value.GenerationChance)
                    resourceList.Add(key: resource.Key, value: resource.Value);
            }

            return resourceList;
        }

        internal static ObjectResources CalculateResourceStates(Dictionary<byte, Resource> resourcesPresent, float surfaceTemperature)
        {
            ObjectResources objectResources = new ObjectResources();

            foreach (KeyValuePair<byte, Resource> resource in resourcesPresent) {
                if (surfaceTemperature < resource.Value.MeltingPoint)
                    objectResources.AddSolid(id: resource.Key, resource: resource.Value);
                else if (surfaceTemperature < resource.Value.BoilingPoint)
                    objectResources.AddLiquid(id: resource.Key, resource: resource.Value);
                else
                    objectResources.AddAtmospheric(id: resource.Key, resource: resource.Value);
            }

            return objectResources;
        }

        internal static Dictionary<byte, Resource> CalculateResourceAbundance(Dictionary<byte, Resource> resourceList)  //Todo: Function is supposed to be calculating the abundance of each resource but it never actually appears to be calculated and is never stored in the Dictonary entry.
        {
            if (resourceList.Count == 0) return new Dictionary<byte, Resource>();
            int totalResourceCount = resourceList.Count;
            byte percentPerResource = (byte)(100 / totalResourceCount);
            byte percentAllocated = 0;
            int resourceUpperLimit = percentPerResource + 5, resourceLowerLimit = percentPerResource - 5;
            Random random = new Random(Seed: DateTime.Now.Millisecond);
            foreach (KeyValuePair<byte, Resource> resource in resourceList)
            {
                if (totalResourceCount == 1)
                {
                    resource.Value.PercentTotalVolume = (byte)(100 - percentAllocated);
                    break;
                }
                resource.Value.PercentTotalVolume = (byte)random.Next(minValue: resourceLowerLimit, maxValue: resourceUpperLimit);
                percentAllocated += resource.Value.PercentTotalVolume;
                totalResourceCount--;
                percentPerResource = (byte)((100 - percentAllocated) / totalResourceCount);
                resourceUpperLimit = percentPerResource + 5;
                resourceLowerLimit = percentPerResource - 5;
            }

            return resourceList;    //TODO: Convert this to no longer have a return type. Modifications should stick due to Dictionaries being reference objects.
        }   //Todo:Resources abundance cannot be stored in the key of the Dictionary entry. 

        internal static double CalculateMass(double volume, float density)
        {
            return volume * density;
        }

        internal static double CalculateSolarInsolation(float auFromStar, Star parentStar)
        {
            double solarInsolation = Math.Pow(x: parentStar.Radius / (auFromStar * AstroPhysicsUtils.ConversionsAndConstants.MetersPerAu), y: 2) * parentStar.SolarFlux;
            return solarInsolation;
        }

        internal static float CalculateBaseTemperature(double solarInsolation, float albedo = .31F)
        {
            double solarTempAtDistance = (float)Math.Pow(x: solarInsolation * (1 - albedo) / (4 * StefanBoltzmannConstant), y: .25);
            return (float)solarTempAtDistance;
        }

        internal static float CalculateAverageDensity(Dictionary<byte, Resource> resourcesPresent)
        {
            double totalDensity = 0;
            byte resourceCount = 0;

            foreach (KeyValuePair<byte, Resource> resource in resourcesPresent)
            {
                totalDensity += resource.Value.Density;
                resourceCount++;
            }

            return (float)(totalDensity / resourceCount);
        } //**UNFINISHED**//
    }

}

