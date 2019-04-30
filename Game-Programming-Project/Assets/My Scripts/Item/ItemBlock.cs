using System.Collections;
using UnityEngine;

public class ItemBlock : MonoBehaviour
{
    #region Variables

    [Header("Respawnable")]
    [SerializeField] private float timeToSpawn;
    [SerializeField] private bool respawnable;
    [SerializeField] private float respawnTime;

    [Header("Debug")]
    [SerializeField] private bool debugMode;
    [SerializeField] private string itemName;

    private Coroutine coroutine;
    private Collider2D[] colliders;
    private SpriteRenderer sr;
    private float spawnSpeed = 0.5f;

    #endregion

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        colliders = GetComponents<Collider2D>();

        SetColliders(false);

        if (debugMode) Invoke("Spawn", timeToSpawn);  
    }

    public void ShowBlock()
    {
        Invoke("Spawn", timeToSpawn);
    }

    private void Spawn()
    {
        coroutine = StartCoroutine(FadeTo(1f, spawnSpeed));
    }

    private void SetColliders(bool state)
    {
        foreach (Collider2D c in colliders) c.enabled = state;
        if (!state) sr.color = new Color(1, 1, 1, 0);
    }

    private IEnumerator FadeTo(float newAlpha, float fadeTime)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioManager.INSTANCE.Play("Item Box");
            SpawnItem(other.transform);

            if (!respawnable) Destroy(gameObject);
            else
            {
                if (coroutine != null) StopCoroutine(coroutine);
                SetColliders(false);
                Invoke("Respawn", respawnTime);
            }
        }
    }

    private void SpawnItem(Transform player)
    {
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

    private void Respawn()
    {
        coroutine = StartCoroutine(FadeTo(1f, spawnSpeed));
    }
}