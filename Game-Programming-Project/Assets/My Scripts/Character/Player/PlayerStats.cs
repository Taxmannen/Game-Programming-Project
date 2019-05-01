using System.Collections;
using UnityEngine;

public class PlayerStats : Character
{
    #region Variables
    [Header("Setup")]
    [SerializeField] private Transform otherPlayer;
    [SerializeField] private Transform goal;

    [Header("Particles")]
    [SerializeField] private ParticleSystem powerParticle;
    [SerializeField] private ParticleSystem restoreParticle;
    [SerializeField] private ParticleSystem stunParticle;
    [SerializeField] private ParticleSystem invertedParticle;

    private PlayerController pc;
    private PlayerJump pj;
    private PlayerDash pd;
    private Animator anim;
    private Coroutine restoreCoroutine;
    private Coroutine stunCoroutine;
    private Coroutine invertedCoroutine;

    private bool activated;
    private float distanceToStartLoserReward = 7.5f; // 15?

    public bool restoring { get; private set; }

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
        pd = GetComponent<PlayerDash>();
    }

    private void Update()
    {
        SetDistances();
    }

    public void Restore(float time)
    {
        if (restoreCoroutine != null) StopCoroutine(restoreCoroutine);
        restoreCoroutine = StartCoroutine(Restoring(time));
    }

    private IEnumerator Restoring(float time)
    {
        restoreParticle.Play();
        UnstunPlayer();
        UninvertPlayer();
        restoring = true;
        yield return new WaitForSecondsRealtime(time);
        restoreParticle.Stop();
        restoring = false;
    }

    public void StunPlayer(float stunTime)
    {
        if (!restoring)
        {
            if (stunCoroutine != null) StopCoroutine(stunCoroutine);
            stunCoroutine = StartCoroutine(Stun(stunTime));
        }
    }

    private void UnstunPlayer()
    {
        if (stunCoroutine != null)
        {
            stunParticle.gameObject.SetActive(false);
            stunParticle.gameObject.SetActive(true);
            StopCoroutine(stunCoroutine);
            anim.SetBool("IsStunned", false);
            pc.SetUnableToMove(false);
        }
    }

    private IEnumerator Stun(float stunTime)
    {
        anim.SetBool("IsStunned", true);
        pc.SetUnableToMove(true);
        pd.StopDash();
        stunParticle.Play();
        yield return new WaitForSeconds(stunTime);

        UnstunPlayer();
    }

    public void InvertPlayer(float invertTime)
    {
        if (!restoring)
        {
            if (invertedCoroutine != null) StopCoroutine(invertedCoroutine);
            invertedCoroutine = StartCoroutine(Invert(invertTime));
        }
    }

    private void UninvertPlayer()
    {
        if (invertedCoroutine != null)
        {
            invertedParticle.Stop();
            StopCoroutine(invertedCoroutine);
            pc.SetInvert(false);
        }
    }

    private IEnumerator Invert(float invertTime)
    {
        pc.SetInvert(true);
        invertedParticle.Play();

        yield return new WaitForSecondsRealtime(invertTime);

        UninvertPlayer();
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
        powerParticle.Play();
        pc.SetBonusSpeed(1.25f);
        pj.SetBonusJumpPower(1.25f);
        activated = true;
    }

    private void DeactiveLoserPowerUp()
    {
        powerParticle.Stop();
        pc.SetBonusSpeed(1);
        pj.SetBonusJumpPower(1);
        activated = false;
    }

    public float DistanceToOtherPlayer { get; private set; }
    public float DistanceToGoal { get; private set; }
    public float OtherPlayersDistanceToGoal { get; private set; }
    public Transform OtherPlayer { get { return otherPlayer; } }
}