using System.Collections.Generic;
using Space_Explorer.main.Resources;

namespace Space_Explorer.main.Structures
{
    public class Structure
    {
        public byte Id;
        public string Name;
        public string Description;
        public byte TechnologicalLevel;
        public ConstructionMaterials CoreConstructionMaterial;
        //Todo: Create a class specifically for defining the "energy" resource as it does not fit nicely into the pre-existing "Resources" class

        public List<RequiredResource> ResourcesRequiredToConstruct = new List<RequiredResource>();
        public byte ConstructionTimeModifier;
        public int StructureHitpoints;
        public sbyte PollutionGeneration;
    }

    public class RequiredResource
    {
        public Resource Resource;
        public int Amount;
    }

    public enum StructureCategories
    {
        PowerGeneration,
        ResourceCollection,
        ResourceProcessing,
        Research,
        PublicServices,
        Housing,
        ComponentProduction,
        VehicleProduction
    }

    public enum ConstructionMaterials : byte
    {
        Wood = 1,
        Stone = 25,
        Steel = 40,
        Plastic = 15,
        Plasteel = 50,
        SmartPlastic = 20
    }


}
