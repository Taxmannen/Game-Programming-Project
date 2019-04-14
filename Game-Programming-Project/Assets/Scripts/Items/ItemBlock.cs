using UnityEngine;

public class ItemBlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();

            string itemName = "Low Drop Chance";
            if (playerStats.DistanceToGoal < playerStats.OtherPlayersDistanceToGoal)
            {
                itemName = "Low Drop Chance";
            }
            else if (playerStats == null) //Tillfälligt
            {
                itemName = "Medium Drop Chance";
            }
            else if (playerStats.DistanceToGoal > playerStats.OtherPlayersDistanceToGoal)
            {
                itemName = "High Drop Chance";
            }
            LootDropData data = Resources.Load<LootDropData>("Loot Drop Data/" + itemName);
            data.DropItem(transform.position);
            Destroy(gameObject);
        }
    }
}