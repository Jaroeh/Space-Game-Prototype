using System.Security.Cryptography;
using System.Security.RightsManagement;

namespace Game_Design_Planner
{/*
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class Database : DbContext
    {
        // Your context has been configured to use a 'Database' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Game_Design_Planner.Database' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Database' 
        // connection string in the application configuration file.
        public Database()
            : base("name=Database")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

    public class BaseObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Resources
    {
        public class Resource : BaseObject
        {
            public byte Category { get; set; }
            public byte Quality { get; set; }
            public short BoilingPoint { get; set; }
            public short MeltingPoint { get; set; }
            public float Density { get; set; }
        }

        public class Resource : Resource
        {
            public byte PercentTotalVolume;
            public double ResourceTotalVolume;
        }

    }

    public class Buildings
    {
        public class Building : BaseObject
        {
            public List<ushort> InputsList { get; set; } //List of resource IDs
            public List<ushort> OutputsList { get; set; } //List of resource IDs
            public sbyte PowerGeneration { get; set; }
            public sbyte PollutionGeneration { get; set; }
            public byte BuildingCategory { get; set; }
        }
    }

    public class SolarBodies
    {
        public enum StarType    //Stars stage of development
        {
            MainSequence = 5,   //Normal life
            GiantStar = 3,      //Low mass star near end of life
            SuperGiant = 1,     //High mass star near end of life
            WhiteDwarf = 0,     //Dying remanent of an imploded star
        }
        public enum StarClassification
        {
            ClassO, //Blue
            ClassB, //Blue-White
            ClassA, //White
            ClassF, //Yellow-White
            ClassG, //Sun
            ClassK, //Orange Dwarf
            ClassM, //Red Dwarf/Red Giant/Red Supergiant
            ClassL, //Brown Dwarf
            ClassD  //White Dwarf
        }
        public class Star : BaseObject
        {
            public StarClassification Class;
            public double Temperature;


        }
        public class BaseBody : BaseObject
        {
            public long ParentObject { get; set; }
            public virtual List<Resources.Resource> SolidResourcesPresent { get; set; }
            public virtual List<Resources.Resource> AtmosResourcesPresent { get; set; } //List of resource IDs
            public virtual List<Resources.Resource> LiquidResourcesPresent { get; set; }
            public short AverageSurfaceTemperature { get; set; }
            public short BaseTemperature { get; set; }
            public float AverageDensity { get; set; }
            public uint Radius { get; set; }
            public double Volume { get; set; }
            public UInt64 Mass { get; set; }        //Stored in Petagrams
            public double Gravity { get; set; }
            public short RotationalPeriod { get; set; }
            public byte AxialTilt { get; set; }
            public byte Classification { get; set; }
            public byte[] ChildMoons { get; set; }
            public byte[] ChildRings { get; set; }
        }

        public class Planetoid : BaseObject
        {
            public bool BreathableAtmosphere { get; set; }
            public byte AtmosphericDensity { get; set; }
            public byte GeologicActivityLevel { get; set; }
            public byte LandmassRatio { get; set; }
            public byte BioDiversity { get; set; }
        }

        public class Planet : Planetoid
        {
            public float AuFromStar { get; set; }
        }

        public class Asteroid : BaseBody
        {
            public float AuFromStar { get; set; }
        }

        public class Moon : Planetoid
        {
            public float KilometersFromParent { get; set; }
            
        }
    }

    public class ObjectContext : DbContext
    {
        public DbSet<Resources.Resource> Resources { get; set; }
        public DbSet<SolarBodies.Planet> Planets { get; set; }
    }*/
}