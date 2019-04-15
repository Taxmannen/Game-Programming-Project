using System.Collections;
using UnityEngine;

public class ItemBlock : MonoBehaviour
{
    public float timeToSpawn;

    [Header("Debug")]
    public bool debugMode;

    private Collider2D[] colliders;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        colliders = GetComponents<Collider2D>();

        SetColliders(false);
        sr.color = new Color(1, 1, 1, 0);

        if (debugMode) Invoke("Spawn", timeToSpawn);  
    }

    public void ShowBlock()
    {
        Invoke("Spawn", timeToSpawn);
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyUp(KeyCode.T))
            {
                StartCoroutine(FadeTo(0.0f, 1.0f));
            }
            if (Input.GetKeyUp(KeyCode.F))
            {
                StartCoroutine(FadeTo(1.0f, 1.0f));
            }
        }
    }

    private void Spawn()
    {
        StartCoroutine(FadeTo(1f, 1f));
    }

    private void SetColliders(bool state)
    {
        foreach (Collider2D c in colliders) c.enabled = state;
    }

    private IEnumerator FadeTo(float newAlpha, float fadeTime)
    {
        float alpha = sr.color.a;
        for (float time = 0.0f; time < 1.0f; time += Time.deltaTime / fadeTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, newAlpha, time));
            sr.color = newColor;
            if (newAlpha == 1 && sr.color.a > 0.75f && !colliders[0].enabled)
            {
                foreach (Collider2D c in colliders) SetColliders(true);
            }
            yield return null;
        }
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
            LootDropData data = Resources.Load<LootDropData>("Loot Drop Data/" + itemName);
            data.DropItem(transform.position, other.transform, other.GetComponent<PlayerStats>().otherPlayer);
            Destroy(gameObject);
        }
    }
}