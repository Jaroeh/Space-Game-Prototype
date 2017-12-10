namespace Space_Explorer.main.utils {

    public class Coordinate
    {
        private readonly int _x;
        private readonly int _y;
        private readonly int _z;

        public Coordinate(int x, int z, int y)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public int GetX() {
            return _x;
        }

        public int GetY() {
            return _y;
        }

        public int GetZ() {
            return _z;
        }
    }
}