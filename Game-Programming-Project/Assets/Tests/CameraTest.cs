using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Cinemachine;

namespace Tests
{
    public class CameraTest
    {
        private GameObject cameraPlayer1;
        private GameObject cameraPlayer2;
        private CinemachineVirtualCamera virtualCam1;
        private CinemachineVirtualCamera virtualCam2;

        [SetUp]
        public void Setup()
        {
            cameraPlayer1 = GameObject.Find("Camera 1");
            cameraPlayer2 = GameObject.Find("Camera 2");
            virtualCam1 = cameraPlayer1.GetComponentInChildren<CinemachineVirtualCamera>();
            virtualCam2 = cameraPlayer2.GetComponentInChildren<CinemachineVirtualCamera>();
        }


        [UnityTest]
        public IEnumerator CheckIfCameraOneExcits()
        {
            Assert.IsNotNull(cameraPlayer1);
            yield return null;
        }

        [UnityTest]
        public IEnumerator CheckIfCameraTwoExcits()
        {
            Assert.IsNotNull(cameraPlayer2);
            yield return null;
        }

        [UnityTest]
        public IEnumerator CheckIfCameraOneHasVirtualCamera()
        {
            Assert.IsNotNull(virtualCam1);
            yield return null;
        }

        [UnityTest]
        public IEnumerator CheckIfCameraTwoHasVirtualCamera()
        {
            Assert.IsNotNull(virtualCam2);
            yield return null;
        }


        [TearDown]
        public void TearDown()
        {
            cameraPlayer1 = null;
            cameraPlayer2 = null;
        }
    }
}
