using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GoalTest
    {
        private Goal goalScript;
        private GameObject goal;
        private Collider2D collider;

        [SetUp]
        public void Setup()
        {
            goal = GameObject.Find("Goal");
            goalScript = goal.GetComponent<Goal>();
            collider = goal.GetComponent<Collider2D>();
        }

        [UnityTest]
        public IEnumerator CheckIfGoalExistsInLevel()
        {
            Assert.IsNotNull(goal);
            yield return null;
        }

        [UnityTest]
        public IEnumerator CheckIfGoalObjectCointainsGoalScript()
        {
            Assert.IsNotNull(goalScript);
            yield return null;
        }

        [UnityTest]
        public IEnumerator CheckIfGoalObjectCointainsTrigger()
        {
            Assert.IsTrue(collider.isTrigger);
            yield return null;
        }

        [TearDown]
        public void TearDown()
        {
            goal = null;
            goalScript = null;
            collider = null;
        }
    }
}