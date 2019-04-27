﻿using UnityEngine;

public class StunPlayer : Item
{
    [SerializeField] private float stunLength = 3;

    private float speedMultiplier = 1;

    private void Update()
    {
        MoveTowardsPlayer(speedMultiplier);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (otherPlayer != null && other.gameObject.name == otherPlayer.name)
        {
            other.GetComponent<PlayerStats>().StunPlayer(stunLength);
            base.UseItem();
        }
    }
}