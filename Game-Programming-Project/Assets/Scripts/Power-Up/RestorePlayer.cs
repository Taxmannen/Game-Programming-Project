using UnityEngine;

public class RestorePlayer : Item
{
    private void Start()
    {
        UseItem();    
    }

    public override void UseItem()
    {
        player.GetComponent<PlayerStats>().Restore();
        base.UseItem();
    }
}