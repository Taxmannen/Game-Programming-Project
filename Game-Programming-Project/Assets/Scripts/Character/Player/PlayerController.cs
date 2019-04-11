using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float groundMovementSpeed;
    public float airMovementSpeed;
    public float smoothing;

    [Header("Jump")]
    public float jumpPower;
    public float jumpTime = 1;

    [Header("Fall")]
    public float fallMultiplier;
    public float lowJumpMultiplier;

    [Header("Layers")]
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 velocity = Vector3.zero;

    private bool facingRight;
    private bool grounded;
    private bool doubleJump;
    private float x;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        facingRight = true;
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal" + " " + gameObject.name) * (grounded ? groundMovementSpeed : airMovementSpeed);
        if (Input.GetButtonDown("Jump" + " " + gameObject.name))
        {
            if (grounded || doubleJump) StartCoroutine(JumpRoutine());
        }

        grounded = Physics2D.OverlapBox(transform.position, new Vector3(0.8f, 0.1f, 0), 0, groundLayer);
        anim.SetBool("IsJumping", !grounded);

        if (!doubleJump && grounded) doubleJump = true;
    }

    private void FixedUpdate()
    {
        Movement();
        Fall();
    }

    public void Movement()
    {
        Vector3 targetVelocity = new Vector2((x * Time.fixedDeltaTime) * 10f, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothing);
        if (x < 0 && facingRight || x > 0 && !facingRight) facingRight = Flip();
        anim.SetFloat("Speed", Mathf.Abs(x));
    }

    private void Fall()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump" + " " + gameObject.name))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

  

    IEnumerator JumpRoutine()
    {
        rb.velocity = Vector2.zero;
        float timer = 0;

        while (Input.GetButton("Jump" + " " + gameObject.name) && timer < jumpTime)
        {
            float velocityY = Mathf.Sqrt(jumpPower * Mathf.Abs(Physics2D.gravity.y));
            rb.velocity = new Vector2(rb.velocity.x, velocityY);

            timer += Time.deltaTime;
            yield return null;
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(0.8f, 0.1f, 0));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.collider);
        }
    }

    public bool GetFacingRight()
    {
        return facingRight;
    }
}