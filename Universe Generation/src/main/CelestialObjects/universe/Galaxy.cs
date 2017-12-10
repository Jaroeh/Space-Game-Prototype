using Space_Explorer.main.utils;

namespace Space_Explorer.main.CelestialObjects.universe {
    public class Galaxy
    {
        public ushort Id;
        public string Name;
        public string Description;

        public int SectorCount = 0, StarSystemCount = 0, PlanetCount = 0, AsteroidCount = 0, MoonCount = 0, MoonACount = 0, MoonBCount = 0, MoonCCount = 0;

        public const byte XSize = 11;
        public const byte ZSize = 11;
        public const byte YSize = 1;

        public static byte GalacticCenterX { get; } = 0;
        public static byte GalacticCenterZ { get; } = 0;
        public static byte GalacticCenterY { get; } = 0;

        public static double GalacticCoreDensity { get; } = .75;

        public readonly Sector[,,] Sectors;

        public Galaxy(ushort id, string name, string description)
        {
            Universe.GalaxyCount++;
            Id = id;
            Name = name;
            Description = description;
            Sectors = new Sector[XSize, ZSize, YSize];

            for (byte xIndex = 0; xIndex < XSize; xIndex++)       //Looping through the x-axis of the galaxy
            {
                for (byte zIndex = 0; zIndex < ZSize; zIndex++)   //Looping through the Z-axis of the galaxy
                {
                    for (byte yIndex = 0; yIndex < YSize; yIndex++)
                    {
                        string sectorId = string.Concat(str0: xIndex.ToString(), str1: "-", str2: zIndex.ToString()); //Todo:Replace this with code that will calculate the number of the sector based on it's positional coordinates (Left->Right:Front->Back:Top->Bottom) Should be a separate function that can be reused with both 3D and 2D coordinates.
                        Sectors[index0: xIndex, index1: zIndex, index2: yIndex] = new Sector(id: sectorId, name: string.Concat(str0: xIndex.ToString(), str1: "-", str2: zIndex.ToString()), description: "Placeholder Description", sectorCoordinates: new Coordinate(x: xIndex, z: zIndex, y: yIndex));    //Generating a new sector and storing it at the current X,Z coordinates
                    }
                }
            }
        }
        
    }
}