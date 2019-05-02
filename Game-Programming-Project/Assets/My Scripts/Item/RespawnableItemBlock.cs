using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnableItemBlock : ItemBlock
{
    [Header("Respawnable")]
    [SerializeField] private float respawnTime;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        colliders = GetComponents<Collider2D>();
        SetColliders(false);
        Invoke("Spawn", timeToSpawn);
    }

    private void Respawn()
    {
        coroutine = StartCoroutine(FadeTo(1f, spawnSpeed));
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnItem(other.transform);
            if (coroutine != null) StopCoroutine(coroutine);
            Invoke("Respawn", respawnTime);
        }
    }
}