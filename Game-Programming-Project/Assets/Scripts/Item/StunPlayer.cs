﻿using UnityEngine;

public class StunPlayer : Item
{
    public float stunLength = 3;

    private float speedMultiplier = 1;

    private void Update()
    {
        if (otherPlayer != null)
        {
            speedMultiplier += Time.deltaTime;
            float step = (10 * Time.deltaTime) * speedMultiplier;
            transform.position = Vector2.MoveTowards(transform.position, otherPlayer.position, step);
        }
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