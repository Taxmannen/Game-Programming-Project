using System.Collections;
using UnityEngine;

public class PlayerStats : Character
{
    [Header("Setup")]
    public Transform otherPlayer;
    public Transform goal;

    private Rigidbody2D rb;
    private Animator anim;
    private Coroutine coroutine;

    public PlayerStats()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetDistances();
    }

    public void StunPlayer(float stunTime)
    {
        coroutine = StartCoroutine(Stun(stunTime));
    }

    public void UnstunPlayer()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            Unstun();
        }
    }

    public IEnumerator Stun(float stunTime)
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetBool("IsStunned", true);

        yield return new WaitForSeconds(stunTime);

        Unstun();    
    }

    private void Unstun()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        anim.SetBool("IsStunned", false);
    }

    public void SetDistances()
    {
        DistanceToGoal             = Vector2.Distance(transform.position, goal.position);
        DistanceToOtherPlayer      = Vector2.Distance(transform.position, otherPlayer.position);
        OtherPlayersDistanceToGoal = Vector2.Distance(otherPlayer.position, goal.position);
    }

    public float DistanceToOtherPlayer { get; private set; }
    public float DistanceToGoal { get; private set; }
    public float OtherPlayersDistanceToGoal { get; private set; }
}