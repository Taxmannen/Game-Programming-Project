using UnityEngine;

public class DangerousObject : MonoBehaviour
{
    [SerializeField] private float stunTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerStats ps = other.GetComponent<PlayerStats>();
            ps.StunPlayer(stunTime);
        }
    }
}