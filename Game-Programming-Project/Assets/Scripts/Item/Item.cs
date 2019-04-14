using UnityEngine;

public class Item : MonoBehaviour
{
    protected Transform player;
    protected Transform otherPlayer;

    public virtual void UseItem()
    {
        Debug.Log("Use Item");
        Destroy(gameObject);
    }

    public void SetPlayers(Transform player, Transform otherPlayer)
    {
        this.player = player;
        this.otherPlayer = otherPlayer;
    }
}