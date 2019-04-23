using System.Collections;
using UnityEngine;

public class TeleportToPlayer : Item
{
    private void Start()
    {
        UseItem();
    }

    public override void UseItem()
    {
        StartCoroutine(Teleport());
    }

    private IEnumerator Teleport()
    {
        //Play Effect;
        yield return new WaitForSeconds(1);
        player.position = otherPlayer.position;
        base.UseItem();
    }
}