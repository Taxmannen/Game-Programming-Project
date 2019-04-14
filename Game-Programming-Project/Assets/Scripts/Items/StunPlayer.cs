using UnityEngine;

public class StunPlayer : Item
{
    void Update()
    {
        if (otherPlayer != null)
        {
            float step = 10 * Time.deltaTime;
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
