using System.Collections;
using UnityEngine;

public class PlayerStats : Character
{
    #region Variables
    [Header("Setup")]
    [SerializeField] private Transform otherPlayer;
    [SerializeField] private Transform goal;
    [SerializeField] private ParticleSystem powerParticles;

    private PlayerController pc;
    private PlayerJump pj;
    private Animator anim;
    private Coroutine coroutine;

    private bool activated;
    private float distanceToStartLoserReward = 5; // 15?
    #endregion

    public PlayerStats()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        pc = GetComponent<PlayerController>();
        pj = GetComponent<PlayerJump>();
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
            pc.SetUnableToMove(false);
        }
    }

    private IEnumerator Stun(float stunTime)
    {
        anim.SetBool("IsStunned", true);
        pc.SetUnableToMove(true);

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
                if (!activated) ActivateLoserPowerup();
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
        pc.SetBonusSpeed(1.25f);
        pj.SetBonusJumpPower(1.25f);
        activated = true;
    }

    private void DeactiveLoserPowerUp()
    {
        powerParticles.Stop();
        pc.SetBonusSpeed(1);
        pj.SetBonusJumpPower(1);
        activated = false;
    }

    public float DistanceToOtherPlayer { get; private set; }
    public float DistanceToGoal { get; private set; }
    public float OtherPlayersDistanceToGoal { get; private set; }
    public Transform OtherPlayer { get { return otherPlayer; } }
}