using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera1;
    [SerializeField] private Camera mainCamera2;

    [SerializeField] private CinemachineVirtualCamera cam1;
    [SerializeField] private CinemachineVirtualCamera cam2;

    public void SetTargetTransform(Transform player1, Transform player2)
    {
        cam1.Follow = player1;
        cam2.Follow = player2;
    }

    public Camera GetCamera(int cam)
    {
        if (cam == 1) return mainCamera1;
        else          return mainCamera2;
    }
}