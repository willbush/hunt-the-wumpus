using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HuntTheWumpus.Tests {
    [TestClass]
    public class GameTests {
        [TestMethod]
        public void IsGameOver_ReturnsTruewhenWumpusIsHit() {
            Assert.IsTrue(Game.IsGameOver(new List<int> { 2, 3, 4, 5, 6 }, 1, 2));
            Assert.IsTrue(Game.IsGameOver(new List<int> { 2, 3, 4, 5, 6 }, 1, 3));
            Assert.IsTrue(Game.IsGameOver(new List<int> { 2, 3, 4, 5, 6 }, 1, 4));
            Assert.IsTrue(Game.IsGameOver(new List<int> { 2, 3, 4, 5, 6 }, 1, 5));
            Assert.IsTrue(Game.IsGameOver(new List<int> { 2, 3, 4, 5, 6 }, 1, 6));
        }

        [TestMethod]
        public void IsGameOver_ReturnsFalsewhenWumpusIsNotHit() {
            Assert.IsFalse(Game.IsGameOver(new List<int> { 2, 3, 4, 5, 6 }, 1, 7));
            Assert.IsFalse(Game.IsGameOver(new List<int> { 2, 3, 4, 5, 6 }, 1, 20));
        }

        [TestMethod]
        public void IsGameOver_ReturnsTrueWhenPlayerHitsSelf() {
            Assert.IsTrue(Game.IsGameOver(new List<int> { 2, 3, 4, 5, 1 }, 1, 20));
            Assert.IsTrue(Game.IsGameOver(new List<int> { 2, 3, 4, 5, 1 }, 1, 20));
        }
    }
}