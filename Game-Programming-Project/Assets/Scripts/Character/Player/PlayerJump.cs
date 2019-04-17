﻿using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump")]
    public float jumpPower = 30;
    public float jumpTime = 0.05f;

    [Header("Fall")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2;
 
    [Header("Fall Damage")]
    public float fallBeforeStunned = 2.5f;

    private Rigidbody2D rb;
    private PlayerController pc;
    private PlayerStats ps;

    private bool doubleJump;
    private float inAirTimer;

    //FIXAS!!
    private float startFallPosY;
    private bool jumping;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
        ps = GetComponent<PlayerStats>();
        doubleJump = true;
    }

    private void Update()
    {
        if (!ps.stunned && Input.GetButtonDown("Jump" + " " + gameObject.name))
        {
            if (pc.grounded)     StartCoroutine(Jump(false));
            else if (doubleJump) StartCoroutine(Jump(true));
        }

        if (pc.grounded)
        {
            if (inAirTimer != 0)
            {
                //Debug.Log("Time in Air:" + " " + inAirTimer);
                //if (inAirTimer > fallBeforeStunned) ps.StunPlayer(1);
                if (!doubleJump) doubleJump = true;
                if (Mathf.Abs(startFallPosY - transform.position.y) > fallBeforeStunned)
                {
                    Debug.Log("FallLength:" + " " + Mathf.Abs(startFallPosY - transform.position.y));
                    ps.StunPlayer(1);
                }
                startFallPosY = 0;
                inAirTimer = 0;
            }
        }
        else
        {
            if (inAirTimer == 0) startFallPosY = transform.position.y;
            inAirTimer += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Fall();
    }

    private IEnumerator Jump(bool isDoubleJump)
    {
        rb.velocity = Vector2.zero;
        jumping = true;

        float timer = 0;
        while (Input.GetButton("Jump" + " " + gameObject.name) && timer < jumpTime)
        {
            float velocityY = Mathf.Sqrt(jumpPower * Mathf.Abs(Physics2D.gravity.y));
            rb.velocity = new Vector2(rb.velocity.x, velocityY);

            timer += Time.deltaTime;
            yield return null;
        }
        if (isDoubleJump) doubleJump = false;

        jumping = false;
    }

    private void Fall()
    {
        if (rb.velocity.y < 0)
        {
            //Debug.Log("Fall Multiplier");
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !jumping)
        {
            //Debug.Log("Jump Multiplier");
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}