using System.Collections;
using UnityEngine;

public class ItemBlock : MonoBehaviour
{
    #region Variables
    [Header("Item Block")]
    [SerializeField] protected float timeToSpawn;

    [Header("Debug")]
    /*[SerializeField]**/ private string itemName = "";

    protected Coroutine coroutine;
    protected Collider2D[] colliders;
    protected SpriteRenderer sr;

    protected float spawnSpeed = 0.5f;

    #endregion

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        colliders = GetComponents<Collider2D>();
        SetColliders(false);
    }

    public void ShowBlock()
    {
        Invoke("Spawn", timeToSpawn);
    }

    private void Spawn()
    {
        coroutine = StartCoroutine(FadeTo(1f, spawnSpeed));
    }

    protected IEnumerator FadeTo(float newAlpha, float fadeTime)
    {
        float alpha = sr.color.a;
        for (float time = 0.0f; time < 1.0f; time += Time.deltaTime / fadeTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, newAlpha, time));
            sr.color = newColor;
            if (newAlpha == 1 && sr.color.a > 0.25f && !colliders[0].enabled)
            {
                foreach (Collider2D c in colliders) SetColliders(true);
            }
            yield return null;
        }
        coroutine = null;
    }

    protected void SetColliders(bool state)
    {
        foreach (Collider2D c in colliders) c.enabled = state;
        if (!state) sr.color = new Color(1, 1, 1, 0);
    }

    protected void SpawnItem(Transform player)
    {
        SetColliders(false);
        AudioManager.INSTANCE.Play("Item Box");
        if (itemName.Length == 0)
        {
            PlayerStats playerStats = player.GetComponent<PlayerStats>();

            string itemName;
            if (playerStats.DistanceToGoal < playerStats.OtherPlayersDistanceToGoal)
            {
                itemName = "Low Drop Chance";
            }
            else if (playerStats.OtherPlayersDistanceToGoal - playerStats.DistanceToGoal < 30)
            {
                itemName = "Medium Drop Chance";
            }
            else itemName = "High Drop Chance";
            Resources.Load<LootDropData>("Loot Drop Data/" + itemName).DropItem(player);
        }
        else player.GetComponent<PlayerInventory>().AddItem(Resources.Load<GameObject>("Items/" + itemName + " " + "Item"));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnItem(other.transform);
            Destroy(gameObject);
        }
    }
}