using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBlockSpawner : MonoBehaviour
{
    private ItemBlock itemBlock;

    private void Start()
    {
        itemBlock = transform.parent.GetComponent<ItemBlock>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerStats ps = other.GetComponent<PlayerStats>();
            if (ps.DistanceToGoal > ps.OtherPlayersDistanceToGoal)
            {
                itemBlock.ShowBlock();
                Destroy(this);
            }
        }
    }
}
