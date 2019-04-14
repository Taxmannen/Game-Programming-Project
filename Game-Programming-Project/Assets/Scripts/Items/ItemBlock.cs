using UnityEngine;

public class ItemBlock : MonoBehaviour
{
    public float timeToSpawn;

    private Collider2D[] colliders;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        colliders = GetComponents<Collider2D>();

        SetState(false);

        Invoke("Spawn", timeToSpawn);
    }

    private void Spawn()
    {
        SetState(true);
    }

    private void SetState(bool state)
    {
        foreach(Collider2D c in colliders) c.enabled = state;
        sr.enabled = state;
    }

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
            else if (playerStats.DistanceToGoal > playerStats.OtherPlayersDistanceToGoal)
            {
                itemName = "Medium Drop Chance";
            }
            else if (playerStats.DistanceToGoal > playerStats.OtherPlayersDistanceToGoal) //Tillfällig
            {
                itemName = "High Drop Chance";
            }
            LootDropData data = Resources.Load<LootDropData>("Loot Drop Data/" + itemName);
            data.DropItem(transform.position, other.transform, other.GetComponent<PlayerStats>().otherPlayer);
            Destroy(gameObject);
        }
    }
}