using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class PlayerTest
    {
        PlayerStats player;

        [SetUp]
        public void Setup()
        {
            player = new GameObject().AddComponent<PlayerStats>();
        }


        [Test]
        public void TestRemovePlayerHealth()
        {
            player.RemoveCurrentHealth(1);
            Assert.AreEqual(player.GetCurrentHealth(), player.GetMaxHealth() - 1);
        }

        [Test]
        public void TestPlayerCanDie()
        {
            player.RemoveCurrentHealth(player.GetCurrentHealth());
            Assert.AreEqual(player.GetCurrentHealth(), 0);
        }


        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(player);
        }
    }
}