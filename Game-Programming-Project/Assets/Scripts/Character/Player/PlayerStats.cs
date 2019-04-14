using System.Collections;
using UnityEngine;

public class PlayerStats : Character
{
    private Rigidbody2D rb;
    private Animator anim;
    private Coroutine currentCoroutine;

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

    public void StunPlayer(float stunTime)
    {
        currentCoroutine = StartCoroutine(Stun(stunTime));
    }

    public void UnstunPlayer()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            Unstun();
        }
    }

    public IEnumerator Stun(float stunTime)
    {
        Debug.Log("Stun Player");
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetBool("IsStunned", true);

        yield return new WaitForSeconds(stunTime);

        Unstun();    
    }

    private void Unstun()
    {
        Debug.Log("Unstun Player");
        rb.bodyType = RigidbodyType2D.Dynamic;
        anim.SetBool("IsStunned", false);
    }

    public void SetDistances(float toPlayer, float toGoal, float otherPlayerToGoal)
    {
        DistanceToGoal = toGoal;
        DistanceToOtherPlayer = toPlayer;
        OtherPlayersDistanceToGoal = otherPlayerToGoal;
    }

    public float DistanceToOtherPlayer { get; private set; }
    public float DistanceToGoal { get; private set; }
    public float OtherPlayersDistanceToGoal { get; private set; }
}