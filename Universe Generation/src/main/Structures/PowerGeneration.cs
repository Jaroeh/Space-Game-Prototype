using Space_Explorer.main.Resources;

namespace Space_Explorer.main.Structures
{
    public class PowerGeneration : Structure
    {
        public short PowerGenerated;
        public float FuelConsuptionRate;
        public Resource FuelResource;
        public StructureCategories Category = StructureCategories.PowerGeneration;

        public class BioMass : Structure
        {
            private short _baseHitPoints = 10000;
            public BioMass(byte technologicalLevel, ConstructionMaterials coreConstructionMaterial)
            {
                Id = 0;
                Name = @"BioMass Power Generator";
                Description = "Placeholder";
                TechnologicalLevel = technologicalLevel;
                CoreConstructionMaterial = coreConstructionMaterial;
                StructureHitpoints = _baseHitPoints * (byte) CoreConstructionMaterial * TechnologicalLevel;

            }
        }

        public class Hydrokinetic : Structure
        {
        }

        public class Wind : Structure
        {
        }

        public class Solar : Structure
        {
        }

        public class Fission : Structure
        {
        }

        public class Fusion : Structure
        {
        }

        public class ZeroPoint : Structure
        {
        }
    }
    
}
