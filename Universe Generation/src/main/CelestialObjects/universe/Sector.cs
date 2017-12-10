using System;
using System.Collections.Generic;
using System.Threading;
using Space_Explorer.main.utils;

namespace Space_Explorer.main.CelestialObjects.universe
{
    public class Sector
    {
        public new string Id;
        public string Name;
        public string Description;
        public Coordinate SectorCoordinates;

        const byte XRadius = 5, ZRadius = 5, YRadius = 0;   //Setting the height, width, and depth of all sectors (each intersect is a potential system location)
        const byte XSize = XRadius * 2 + 1;                 //The total size is (radius * 2 + 1). The +1 is to account for a galactic center of 0,0,0
        const byte ZSize = ZRadius * 2 + 1;
        const byte YSize = YRadius * 2 + 1;
        public double SectorDensity;
        public byte SystemCount;
        public double DistanceFromCore;
        public readonly global::Space_Explorer.main.CelestialObjects.universe.System[,,] SectorGrid;

        private const float UpperSystemCountMultiplier = 1.15F, LowerSystemCountMultiplier = .85F;

        public Sector(string id, string name, string description, Coordinate sectorCoordinates)
        {
            Universe.SectorCount++;
            Id = id;
            Name = name;
            Description = description;

            SectorGrid = new global::Space_Explorer.main.CelestialObjects.universe.System[XSize, ZSize, YSize];

            CalculateSectorProperties(sectorCoordinates: sectorCoordinates, xSize: XSize, zSize: ZSize, ySize: YSize);

            List<Coordinate> potentialSystemCoordinates = CreateListOfSectorIndexes(sectorXSize: XSize, sectorZSize: ZSize, sectorYSize: YSize);
            Random random = new Random(Seed: DateTime.Now.Millisecond);                                    //Initializing a new random number generator

            for (int systemsPlaced = 0; systemsPlaced < SystemCount; systemsPlaced++)
            {
                if (potentialSystemCoordinates.Count < 1) break;

                Coordinate systemCoordinates = CalculateSystemCoordinate(sectorIndexes: potentialSystemCoordinates, random: random);
                string sectorId = string.Concat(systemCoordinates.GetX(), ":", systemCoordinates.GetZ(), ":", systemCoordinates.GetY()); //Todo:Replace this with code that will calculate the number of the sector based on it's positional coordinates (Left->Right:Front->Back:Top->Bottom) Should be a separate function that can be reused with both 3D and 2D coordinates.
                SectorGrid[index0: systemCoordinates.GetX(), index1: systemCoordinates.GetZ(), index2: systemCoordinates.GetY()] = new StarSystem(id: sectorId, name: string.Concat(systemCoordinates.GetX(), ":", systemCoordinates.GetZ(), ":", systemCoordinates.GetY()), description: description = "Placeholder");  //Generating a new StarSystem at the new position

                Thread.Sleep(millisecondsTimeout: 1);
            }

            //Filling in the remaining sector grid spaces with empty System objects
            foreach (Coordinate nullSectorGridCoordinate in potentialSystemCoordinates)
            {
                string sectorId = string.Concat(nullSectorGridCoordinate.GetX(), ":", nullSectorGridCoordinate.GetZ(), ":", nullSectorGridCoordinate.GetY()); //Todo:Replace this with code that will calculate the number of the sector based on it's positional coordinates (Left->Right:Front->Back:Top->Bottom) Should be a separate function that can be reused with both 3D and 2D coordinates.
                SectorGrid[index0: nullSectorGridCoordinate.GetX(), index1: nullSectorGridCoordinate.GetZ(), index2: nullSectorGridCoordinate.GetY()] = new global::Space_Explorer.main.CelestialObjects.universe.System(id: sectorId);
            }
        }

        private void CalculateSectorProperties(Coordinate sectorCoordinates, byte xSize, byte zSize, byte ySize)
        {
            Random random = new Random(Seed: DateTime.Now.Millisecond);            //Initializing a new random number generator

            DistanceFromCore = Math.Sqrt(d: Math.Pow(x: sectorCoordinates.GetX() - Galaxy.GalacticCenterX, y: 2) + Math.Pow(x: sectorCoordinates.GetZ() - Galaxy.GalacticCenterZ, y: 2));  //Calculating the sectors distance from the core
            SectorDensity = Math.Pow(x: Galaxy.GalacticCoreDensity, y: DistanceFromCore);                                     //Calculating the density of the sector
            if (SectorDensity == 1 && DistanceFromCore == 0) SectorDensity = .8;                                        //If this sector is the galactic center then set to the density to .8 (because the denisty equation will produce a value of one when the distance to the core is zero)

            int maxSystemsPerSector = xSize * zSize * ySize;                                                    //Calculates the maximum number of system locations given the size of the sector
            byte lowerRangeLimit = (byte)(maxSystemsPerSector * LowerSystemCountMultiplier * SectorDensity);    //Calculating the lower limit to the number of systems to be generated in the current sector
            byte upperRangeLimit = (byte)(maxSystemsPerSector * UpperSystemCountMultiplier * SectorDensity);    //Calculating the upper limit to the number of systems to be generated in the current sector

            SystemCount = (byte)random.Next(minValue: lowerRangeLimit, maxValue: upperRangeLimit);      //Calculating the number of sytems to be placed in the current sector
        }

        private Coordinate CalculateSystemCoordinate(List<Coordinate> sectorIndexes, Random random)
        {
            int selectedIndex = random.Next(minValue: 0, maxValue: sectorIndexes.Count - 1);
            Coordinate newSystemCoordinate = sectorIndexes[index: selectedIndex];
            sectorIndexes.RemoveAt(index: selectedIndex);

            if (SectorGrid[index0: newSystemCoordinate.GetX(), index1: newSystemCoordinate.GetZ(), index2: newSystemCoordinate.GetY()] != null)
            {
                throw new Exception(message: @"Star System placed in occupied sector grid.");
            }

            return newSystemCoordinate;
        }

        private static List<Coordinate> CreateListOfSectorIndexes(byte sectorXSize, byte sectorZSize, byte sectorYSize)
        {
            List<Coordinate> sectorIndexList = new List<Coordinate>();

            for (byte xIndex = 0; xIndex < sectorXSize; xIndex++)
            {
                for (byte zIndex = 0; zIndex < sectorZSize; zIndex++)
                {
                    for (byte yIndex = 0; yIndex < sectorYSize; yIndex++)
                    {
                        Coordinate sectorCoordinate = new Coordinate(x: xIndex, z: zIndex, y: yIndex);
                        sectorIndexList.Add(item: sectorCoordinate);
                    }
                }
            }
            return sectorIndexList;
        }

    }
}
