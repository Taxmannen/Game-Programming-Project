using UnityEngine;
using Cinemachine;

public class Camera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam1;
    [SerializeField] private CinemachineVirtualCamera cam2;

    public void SetTargetTransform(Transform player1, Transform player2)
    {
        cam1.Follow = player1;
        cam2.Follow = player2;
    }
}
