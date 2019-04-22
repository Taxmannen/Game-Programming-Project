using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float groundMovementSpeed = 40;
    [SerializeField] private float airMovementSpeed = 30;
    [SerializeField] private float smoothing = 0.05f;
    [SerializeField] private bool facingRight;

    [Header("Setup")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector3 groundOffset;
    [SerializeField] private Transform raycastFrom;

    [HideInInspector]
    public bool grounded;

    private Rigidbody2D rb;
    private Animator anim;
    private PlayerStats ps;

    private Vector3 velocity = Vector3.zero;

    private bool hittingWall;
    private float x;
    private bool attacked;
    private bool inverted;

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
        x = Input.GetAxis("Horizontal" + " " + gameObject.name) * (grounded ? groundMovementSpeed : airMovementSpeed);
        if (inverted) x = -x;
       
        grounded = Physics2D.OverlapBox(transform.position - groundOffset, new Vector3(0.55f, 0.1f, 0), 0, groundLayer);
        anim.SetBool("IsJumping", !grounded);

        if (raycastFrom != null)
        {
            Vector3 dir = new Vector3(0, 0, 0) { x = facingRight ? 0.15f : -0.15f };
            Debug.DrawRay(raycastFrom.position, dir, Color.red);
            hittingWall = Physics2D.Raycast(raycastFrom.position, dir, Mathf.Abs(dir.x), groundLayer);
        }
    }

    private void FixedUpdate()
    {
        if (!ps.Stunned && !attacked) Movement();
    }

    public void Movement()
    {
        Vector3 targetVelocity = new Vector2((x * Time.fixedDeltaTime) * 10f, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothing);
        if (x < 0 && facingRight || x > 0 && !facingRight) facingRight = Flip();

        if (!hittingWall) anim.SetFloat("Speed", Mathf.Abs(x));
        else              anim.SetFloat("Speed", 0);

        //if (Input.GetKeyDown(KeyCode.Q)) StartCoroutine(InvertedCurse(1));
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
        if (!ps.Stunned) StartCoroutine(Hit(force, time));
    }

    private IEnumerator Hit(Vector2 force, float hitTime)
    {
        float timer = 0;
        rb.velocity = Vector2.zero;
        anim.SetBool("IsHit", true);

        attacked = true;
        while (timer < hitTime)
        {
            rb.velocity = force;
            timer += Time.deltaTime;
            yield return null;
        }
        anim.SetBool("IsHit", false);
        attacked = false;
    }

    private IEnumerator InvertedCurse(float invertedTime)
    {
        inverted = true;
        yield return new WaitForSecondsRealtime(invertedTime);
        inverted = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position - groundOffset, new Vector3(0.55f, 0.1f, 0));
    }

    public bool GetFacingRight() { return facingRight; }
}