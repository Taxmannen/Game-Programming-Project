using UnityEngine;

public class SawBlade : MonoBehaviour
{
    [Header("Blade")]
    [SerializeField] private Transform blade;
    [SerializeField] private float rotationSpeed = 500;

    private void Update()
    {
        if (blade != null) transform.Rotate(new Vector3(0, 0, -(rotationSpeed * Time.deltaTime)));
    }
}