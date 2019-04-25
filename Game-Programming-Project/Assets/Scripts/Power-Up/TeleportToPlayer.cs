using System.Collections;
using UnityEngine;

public class TeleportToPlayer : Item
{
    private Material mat;
    private PlayerController pc;
    private Rigidbody2D rb;

    private void Start()
    {
        mat = player.GetComponent<Material>();
        rb = player.GetComponent<Rigidbody2D>();
        pc = player.GetComponent<PlayerController>();
        UseItem();
    }

    public override void UseItem()
    {
        StartCoroutine(Teleport());
    }

    private IEnumerator Teleport()
    {
        //Play Effect;
        bool startDissolve = true;
        bool endDissolve = true;

        pc.SetUnableToMove(true);
        while (startDissolve)
        {
            startDissolve = false;
        }

        yield return new WaitForSeconds(1);

        player.position = otherPlayer.position;
        while (endDissolve)
        {
            endDissolve = false;
        }
        pc.SetUnableToMove(false);
        base.UseItem();
    }
}