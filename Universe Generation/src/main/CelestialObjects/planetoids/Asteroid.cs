using Space_Explorer.main.CelestialObjects.universe;

namespace Space_Explorer.main.CelestialObjects.planetoids
{
    public class Asteroid : CelestialObject
    {
        public Asteroid(float auFromParent)
        {
            Universe.AsteroidCount++;
            AuFromParent = auFromParent;
            ResourcesPresent = CalculateBodyResources();
            ResourcesByState = CalculateResourceStates(resourcesPresent: ResourcesPresent, surfaceTemperature: AverageSurfaceTemperature);
            ResourcesByState = new ObjectResources(solids: ResourcesByState.GetSolids(), atmospherics: null, liquids: null);
        }
    }
}
