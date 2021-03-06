﻿using UnityEngine;

public class InvertedControls : Powerup
{
    [SerializeField] private float effectTime = 3;

    private float speedMultiplier = 1;

    private void Start()
    {
        AudioManager.INSTANCE.Play("Inverted");    
    }

    private void Update()
    {
        MoveTowardsPlayer(speedMultiplier);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (otherPlayer != null && other.gameObject.name == otherPlayer.name)
        {
            AudioManager.INSTANCE.Play("Hit", pitch: 1.2f);
            other.GetComponent<PlayerStats>().InvertPlayer(effectTime);
            base.UseItem();
        }
    }
}
