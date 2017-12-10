namespace Space_Explorer.main.CelestialObjects.universe {
    public class Universe
    {
        public ushort Id;
        public string Name;
        public string Description;

        public static int GalaxyCount = 0, SectorCount = 0, StarSystemCount = 0, EmptySystemCount = 0, StarCount = 0, PlanetCount = 0, RingCount = 0, AsteroidCount = 0, MoonCount = 0, MoonACount = 0, MoonBCount = 0, MoonCCount = 0;
        public Galaxy Galaxy;

        public Universe(ushort id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;

            Galaxy = new Galaxy(id: 0, name: @"Main Galaxy", description: "Placeholder Description");
        }
    }
}