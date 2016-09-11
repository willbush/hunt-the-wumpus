using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HuntTheWumpus.Tests {
    [TestClass]
    public class DodecahedronMapTests {
        [TestMethod]
        public void GetRandomAvailableRoom_canReturnAll() {
            const int numOfRooms = 20;
            var map = new DodecahedronMap();
            var availableRooms = new HashSet<int>(Enumerable.Range(1, numOfRooms));
            var assignedRooms = new HashSet<int>();
            for (int i = 0; i < numOfRooms; ++i) {
                int roomNum = map.GetRandomAvailableRoom();
                Assert.IsTrue(availableRooms.Remove(roomNum));
                Assert.IsTrue(assignedRooms.Add(roomNum));
            }
            Assert.AreEqual(0, availableRooms.Count);
            Assert.AreEqual(numOfRooms, assignedRooms.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetRandomAvailableRoom_throwsIfTooManyAssignmentsAttempted() {
            const int numOfRooms = 20;
            var map = new DodecahedronMap();

            for (int i = 0; i < numOfRooms + 1; ++i)
                map.GetRandomAvailableRoom();
        }
    }
}