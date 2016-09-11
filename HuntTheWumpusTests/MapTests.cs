using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HuntTheWumpus.Tests {
    [TestClass]
    public class MapTests {
        [TestMethod]
        public void GetRandomAvailableRoom_canReturnAll() {
            const int numOfRooms = 20;
            var availableRooms = new HashSet<int>(Enumerable.Range(1, numOfRooms));
            var occupiedRooms = new HashSet<int>();
            for (int i = 0; i < numOfRooms; ++i) {
                int roomNum = Map.GetRandomAvailableRoom(occupiedRooms);
                Assert.IsTrue(availableRooms.Remove(roomNum));
            }
            Assert.AreEqual(0, availableRooms.Count);
            Assert.AreEqual(numOfRooms, occupiedRooms.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetRandomAvailableRoom_throwsIfTooManyAssignmentsAttempted() {
            const int numOfRooms = 20;
            var occupiedRooms = new HashSet<int>();

            for (int i = 0; i < numOfRooms + 1; ++i)
                Map.GetRandomAvailableRoom(occupiedRooms);
        }
    }
}