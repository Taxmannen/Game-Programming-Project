using UnityEngine;

public class StunBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().StunPlayer(0.5f);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground")) Destroy(gameObject);
    }
}
