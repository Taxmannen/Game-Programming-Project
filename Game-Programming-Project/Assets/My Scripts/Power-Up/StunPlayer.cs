using UnityEngine;

public class StunPlayer : Item
{
    [SerializeField] private float stunLength = 3;

    private float speedMultiplier = 1;

    private void Start()
    {
        AudioManager.Instance.Play("Stun");        
    }

    private void Update()
    {
        MoveTowardsPlayer(speedMultiplier);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (otherPlayer != null && other.gameObject.name == otherPlayer.name)
        {
            AudioManager.Instance.Play("Hit", pitch: 1.2f);
            other.GetComponent<PlayerStats>().StunPlayer(stunLength);
            base.UseItem();
        }
    }
}