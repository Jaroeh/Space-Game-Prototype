using System;
using System.Collections.Generic;

public class Vehicles
{
	public enum WallTypes
	{
		full = 0,
		half,
		none,
		clear,
		force,
		door,
		hatch,
		bulkhead
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
