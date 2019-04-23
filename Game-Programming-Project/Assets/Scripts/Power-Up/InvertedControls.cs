using UnityEngine;

public class InvertedControls : Item
{
    private float speedMultiplier = 1;

    private void Update()
    {
        MoveTowardsPlayer(speedMultiplier);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (otherPlayer != null && other.gameObject.name == otherPlayer.name)
        {
            other.GetComponent<PlayerController>().InvertPlayerControls(true);
            base.UseItem();
        }
    }
}
