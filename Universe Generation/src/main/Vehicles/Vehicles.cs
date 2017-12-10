using System.Collections.Generic;

namespace Space_Explorer.main.Vehicles
{
    public class Vehicle
    {
        public enum WallTypes
        {
            Full = 0,           //Full length standard wall. Automatically placed between rooms
            Railing,            //Railing style wall
            None,               //No wall present. Used between gridsquares of a single room and can be placed manually to open spaces between rooms
            Window,             //Transparent wall segment (multiple types? full length and only partial wall?)
            ContainmentField,   //Deck to ceiling atmospher containment field
            Door,               //Standard sized non-airtight door
            Hatch,              //Standard sized airtight door
            Bulkhead            //Full sized deck to ceiling airtight sliding door
        }

        public static class Room
        {
            public class BaseRoom
            {

            }

            public class CommandCenter : BaseRoom
            {
                public int Var1;
                public bool Var2;
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
            WallTypes _front;
            WallTypes _back;
            WallTypes _left;
            WallTypes _right;
            WallTypes _ceiling;
            WallTypes _floor;
        }

        public class Spacecraft
        {
            public Spacecraft(Room.BaseRoom[,,] vehicleGrid)
            {
                _vehicleGrid = vehicleGrid;
                Room.CommandCenter cmdCenter = new Room.CommandCenter();
                //cmdCenter.
            }

            Room.BaseRoom[,,] _vehicleGrid;
            List<Module> _moduleList = new List<Module>();
            List<Crewmember> _crewList = new List<Crewmember>();
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
}
