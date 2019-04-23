using System.Collections;
using UnityEngine;

public class PlayerStats : Character
{
    #region Variables
    [Header("Setup")]
    [SerializeField] private Transform otherPlayer;
    [SerializeField] private Transform goal;
    [SerializeField] private ParticleSystem powerParticles;

    private Rigidbody2D rb;
    private PlayerController pc;
    private Animator anim;
    private Coroutine coroutine;

    private bool activated;
    private float distanceToStartLoserReward = 15;
    #endregion

    public PlayerStats()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
    }

    private void Update()
    {
        SetDistances();
    }

    public void Restore()
    {
        UnstunPlayer();
        pc.InvertPlayerControls(false);
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

    private void SetDistances()
    {
        DistanceToGoal             = Vector2.Distance(transform.position, goal.position);
        DistanceToOtherPlayer      = Vector2.Distance(transform.position, otherPlayer.position);
        OtherPlayersDistanceToGoal = Vector2.Distance(otherPlayer.position, goal.position);

        if (OtherPlayersDistanceToGoal < DistanceToGoal)
        {
            if (DistanceToGoal - OtherPlayersDistanceToGoal > distanceToStartLoserReward)
            {
                ActivateLoserPowerup();
            }
        }
        else if (DistanceToGoal < OtherPlayersDistanceToGoal)
        {
            if (activated) DeactiveLoserPowerUp();
        }
    }

    private void ActivateLoserPowerup()
    {
        powerParticles.Play();
        activated = true;
    }

    private void DeactiveLoserPowerUp()
    {
        powerParticles.Stop();
        activated = false;
    }

    public float DistanceToOtherPlayer { get; private set; }
    public float DistanceToGoal { get; private set; }
    public float OtherPlayersDistanceToGoal { get; private set; }
    public Transform OtherPlayer { get { return otherPlayer; } }
    public bool Stunned { get; private set; }
}