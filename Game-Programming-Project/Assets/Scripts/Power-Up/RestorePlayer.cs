using UnityEngine;

public class RestorePlayer : Item
{
    private void Start()
    {
        UseItem();    
    }

    public override void UseItem()
    {
        Debug.Log("Use Restore Player!");
        player.GetComponent<PlayerStats>().Restore();
        base.UseItem();
    }
}