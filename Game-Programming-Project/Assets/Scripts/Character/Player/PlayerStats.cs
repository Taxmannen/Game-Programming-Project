using System.Collections;
using UnityEngine;

public class PlayerStats : Character
{
    [Header("Setup")]
    [SerializeField] private Transform otherPlayer;
    [SerializeField] private Transform goal;

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
            anim.SetBool("IsStunned", false);
            Stunned = false;
        }
    }

    private IEnumerator Stun(float stunTime)
    {
        rb.velocity = Vector2.zero;
        anim.SetBool("IsStunned", true);
        Stunned = true;

        yield return new WaitForSeconds(stunTime);

        UnstunPlayer();
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
    public Transform OtherPlayer { get { return otherPlayer; } }
    public bool Stunned { get; private set; }
}