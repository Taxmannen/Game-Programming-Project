using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    #region Variables 
    [Header("Movement")]
    [SerializeField] private float groundMovementSpeed = 40;
    [SerializeField] private float airMovementSpeed = 30;
    [SerializeField] private float smoothing = 0.05f;
    [SerializeField] private bool facingRight;

    [Header("Setup")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector3 groundOffset;
    [SerializeField] private Transform raycastFrom;
    [SerializeField] private ParticleSystem invertParticles;

    [HideInInspector]
    public bool grounded;

    private Rigidbody2D rb;
    private Animator anim;
    private PlayerStats ps;

    private Coroutine invertedCoroutine;
    private Vector3 velocity = Vector3.zero;
    private Vector3 overlapBoxSize = new Vector3(0.55f, 0.1f, 0);

    private bool hittingWall;
    private float x;
    private float bonusSpeed = 1;

    public bool UnableToMove { get; private set; }

    public bool Inverted { get; private set; }

    #endregion

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ps = GetComponent<PlayerStats>();
        grounded = true;

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), ps.OtherPlayer.GetComponent<Collider2D>());
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal" + " " + gameObject.name) * (grounded ? groundMovementSpeed : airMovementSpeed) * bonusSpeed;
        if (Inverted) x = -x;
       
        grounded = Physics2D.OverlapBox(transform.position - groundOffset, overlapBoxSize, 0, groundLayer);
        anim.SetBool("IsJumping", !grounded);

        CheckIfWallInfront();
    }

    private void FixedUpdate()
    {
        if (!UnableToMove) Movement();
    }

    public void Movement()
    {
        Vector3 targetVelocity = new Vector2((x * Time.fixedDeltaTime) * 10f, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothing);
        if (x < 0 && facingRight || x > 0 && !facingRight) facingRight = Flip();

        if (!hittingWall) anim.SetFloat("Speed", Mathf.Abs(x));
        else              anim.SetFloat("Speed", 0);
    }

    private void CheckIfWallInfront()
    {
        if (raycastFrom != null)
        {
            Vector3 dir = new Vector3(0, 0, 0) { x = facingRight ? 0.15f : -0.15f };
            Debug.DrawRay(raycastFrom.position, dir, Color.red);
            hittingWall = Physics2D.Raycast(raycastFrom.position, dir, Mathf.Abs(dir.x), groundLayer);
        }
    }

    private bool Flip()
    {
        Vector3 theScale = transform.localScale;
        bool currentFacingRight = false;

        theScale.x *= -1;
        transform.localScale = theScale;

        if (theScale.x > 0) currentFacingRight = true;
        return currentFacingRight;
    }

    public void HitPlayer(Vector2 force, float time)
    {
        if (!UnableToMove) StartCoroutine(Hit(force, time));
    }

    private IEnumerator Hit(Vector2 force, float hitTime)
    {
        float timer = 0;
        rb.velocity = Vector2.zero;
        anim.SetBool("IsHit", true);
        AudioManager.INSTANCE.Play("Hit", pitch: 1f);
        UnableToMove = true;
        while (timer < hitTime)
        {
            rb.velocity = force;
            timer += Time.deltaTime;
            yield return null;
        }
        anim.SetBool("IsHit", false);
        UnableToMove = false;
    }

    public void InvertPlayerControls(bool status, float time = 0)
    {
        if (status && !ps.restoring) invertedCoroutine = StartCoroutine(InvertedCurse(time));
        else
        {
            if (invertedCoroutine != null)
            {
                StopCoroutine(invertedCoroutine);
                invertedCoroutine = null;
                Inverted = false;
                //invertParticles.Stop();
            }
        }
    }

    private IEnumerator InvertedCurse(float invertedTime)
    {
        Inverted = true;
        //invertParticles.Play();
        yield return new WaitForSecondsRealtime(invertedTime);
        Inverted = false;
        //invertParticles.Stop();
        invertedCoroutine = null;
    }

    public void SetBonusSpeed(float newBonusSpeed)
    {
        bonusSpeed = newBonusSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position - groundOffset, overlapBoxSize);
    }

    public void SetUnableToMove(bool state)
    {
        UnableToMove = state;
        if (state)
        {
            rb.velocity = Vector2.zero;
            anim.SetFloat("Speed", 0);
        }
    }

    public bool GetFacingRight() { return facingRight; }
}