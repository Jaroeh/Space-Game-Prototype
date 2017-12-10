using System;
using System.Collections.Generic;
using Space_Explorer.main.CelestialObjects.planetoids;
using Space_Explorer.main.CelestialObjects.stellar;

namespace Space_Explorer.main.CelestialObjects.universe
{
    //TODO: Rethink System/Starsystem structure and if necessary separate into two separate class files.
    public class System
    {
        public new string Id;
        public string Name;
        public string Description;

        public System(){}

        public System(string id)
        {
            Universe.EmptySystemCount++;
            Id = id;
        }
    }

    public class StarSystem : System
    {
        public Star Star = new Star();
        public List<Planetoid> Planets;
        public List<Asteroid> Asteroids;

        public StarSystem(string id, string name = null, string description = null)
        {
            Universe.StarSystemCount++;
            Id = id;
            Name = name;
            Description = description;

            Planets = GeneratePlanets(parentSystem: this);
            Asteroids = GenerateAsteroids(parentSystem: this);
        }

        private List<Planetoid> GeneratePlanets(StarSystem parentSystem)
        {
            List<Planetoid> planets = new List<Planetoid>();
            Random random = new Random(Seed: DateTime.Now.Millisecond);
            byte numberOfPlanets = (byte)random.Next(minValue: 1, maxValue: 9);
            byte planetId = 0;
            List<float> orbitsUsed = new List<float>();
            float[] planetaryOrbitDistances = new float[numberOfPlanets];       //Holds the orbits of the planets stored in terms of AU's

            for (int index = 0; index < planetaryOrbitDistances.Length; index++)
            {
                float orbitalDistance;
                do
                {
                    byte orbitRange = (byte)random.Next(minValue: 0, maxValue: 100);
                    if (orbitRange < 50)
                    {
                        orbitalDistance = random.Next(minValue: 2, maxValue: 40) / 10F;
                    }
                    else if (orbitRange < 90)
                    {
                        orbitalDistance = random.Next(minValue: 41, maxValue: 70) / 10F;
                    }
                    else
                    {
                        orbitalDistance = random.Next(minValue: 71, maxValue: 500) / 10F;
                    }
                } while (orbitsUsed.Contains(item: orbitalDistance));

                planetaryOrbitDistances[index0: index] = orbitalDistance;
                orbitsUsed.Add(item: orbitalDistance);

                RockyPlanet planet = new RockyPlanet(planetId: planetId, planetName: planetId.ToString(), description: "Placeholder Description", parentStarSystem: parentSystem, parentStar: parentSystem.Star, auFromParent: orbitalDistance);
                planets.Add(item: planet);
            }

            return planets;
        }

        private List<Asteroid> GenerateAsteroids(StarSystem parentSystem)
        {
            //Enter code that will generate a variable number of asteroids from 100 to 1000 per system in orbits that do not intersect with planetary orbits. Apprx 80% should be grouped into a belt.
            List<Asteroid> asteroids = new List<Asteroid>();
            asteroids.Add(item: new Asteroid(auFromParent: .1F));
            return Asteroids;
        }
    }
}
