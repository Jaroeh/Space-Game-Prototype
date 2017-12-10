using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Space_Explorer.main.CelestialObjects;
using Space_Explorer.main.utils;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Space_Explorer.test.CelestialObjects {

    internal class CelestialObjectsTest {

        [Test]
        public void test_CoordinateConstructor() {
            const int x = 10, y = 20, z = 30;

            Coordinate coordinate = new Coordinate(x: x, z: z, y: y);

            Assert.AreEqual(expected: x, actual: coordinate.GetX());
            Assert.AreEqual(expected: y, actual: coordinate.GetY());
            Assert.AreEqual(expected: z, actual: coordinate.GetZ());
        }
    }
}
