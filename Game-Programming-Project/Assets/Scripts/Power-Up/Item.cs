using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite sprite;

    protected Transform player;
    protected Transform otherPlayer;

    public virtual void UseItem()
    {
        Destroy(gameObject);
    }

    public void SetPlayers(Transform player, Transform otherPlayer)
    {
        this.player = player;
        this.otherPlayer = otherPlayer;
    }
    
    protected void MoveTowardsPlayer(float speedMultiplier)
    {
        if (otherPlayer != null)
        {
            speedMultiplier += Time.deltaTime;
            float step = (10 * Time.deltaTime) * speedMultiplier;
            transform.position = Vector2.MoveTowards(transform.position, otherPlayer.position, step);
        }
    }
}