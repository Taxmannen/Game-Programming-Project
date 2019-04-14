
using UnityEngine;

public class Unstun : Item
{
    public override void UseItem()
    {
        Debug.Log("Wake up Player");
        base.UseItem();
    }
}
