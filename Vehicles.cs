using System;

public class Vehicles
{
	public enum WallTypes
	{
		full = 0,           //Full length standard wall. Automatically placed between rooms
		railing,            //Railing style wall
		none,               //No wall present. Used between gridsquares of a single room and can be placed manually to open spaces between rooms
		window,             //Transparent wall segment (multiple types? full length and only partial wall?)
		containmentField,   //Deck to ceiling atmospher containment field
		door,               //Standard sized non-airtight door
		hatch,              //Standard sized airtight door
		bulkhead            //Full sized deck to ceiling airtight sliding door
	}

	public static class Room
	{
		public class BaseRoom
		{
			
		}

		public class CommandCenter : BaseRoom
		{
			public int var1;
			public bool var2;
		}

		internal class Corridor : BaseRoom
		{
			
		}

		internal class Engineering : BaseRoom
		{
			
		}

		internal class CrewQuarters : BaseRoom
		{
			
		}

		internal class WeaponsBay : BaseRoom
		{
			 
		}

		internal class SensorArray : BaseRoom
		{
			
		}

		internal class VehicleBay : BaseRoom
		{
			
		}

		internal class CargoBay : BaseRoom
		{
			
		}

		internal class ShieldGeneratorRoom : BaseRoom
		{
			
		}

		internal class Barracks : BaseRoom
		{
			
		}

		internal class PassengerCabins : BaseRoom
		{
			
		}
	}

	internal class Module
	{
		
	}

	internal class Crewmember
	{
		
	}

	internal class RoomWalls
	{
		WallTypes front;
		WallTypes back;
		WallTypes left;
		WallTypes right;
		WallTypes ceiling;
		WallTypes floor;
	}

	public class Spacecraft
	{
		public Spacecraft(Room.BaseRoom[,,] vehicleGrid)
		{
			VehicleGrid = vehicleGrid;
			VehicleGrid[0, 0, 0] = Room.CommandCenter;
			VehicleGrid[0, 0, 0];
			Room.CommandCenter cmdCenter = new Room.CommandCenter();
			//cmdCenter.
		}

		Room.BaseRoom[,,] VehicleGrid;
		List<Module> ModuleList = new List<Module>();
		List<Crewmember> CrewList = new List<Crewmember>();
	}

	public class GroundVehicle
	{
		
	}

	public class Aircraft
	{
		
	}

	public class NavalShip
	{
		
	}
}
