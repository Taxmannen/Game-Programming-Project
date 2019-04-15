using UnityEngine;

public class DangerousObject : MonoBehaviour
{
    public float stunTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerStats ps = other.GetComponent<PlayerStats>();
            ps.StunPlayer(stunTime);
        }
    }
}
