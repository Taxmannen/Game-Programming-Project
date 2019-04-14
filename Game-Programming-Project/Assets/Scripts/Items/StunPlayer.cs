using UnityEngine;

public class StunPlayer : Item
{
    private float speedMultiplier = 1;

    void Update()
    {

        if (otherPlayer != null)
        {
            speedMultiplier += Time.deltaTime;
            float step = (12 * Time.deltaTime) * speedMultiplier;
            transform.position = Vector2.MoveTowards(transform.position, otherPlayer.position, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (otherPlayer != null && other.gameObject.name == otherPlayer.name)
        {
            other.GetComponent<PlayerStats>().StunPlayer(2);
            base.UseItem();
        }
    }
}
