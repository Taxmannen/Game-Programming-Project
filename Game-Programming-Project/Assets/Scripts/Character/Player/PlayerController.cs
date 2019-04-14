using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float groundMovementSpeed = 40;
    [SerializeField] private float airMovementSpeed = 30;
    [SerializeField] private float smoothing = 0.05f;
    [SerializeField] private bool facingRight;

    [Header("Layers")]
    public LayerMask groundLayer;

    [HideInInspector]
    public bool grounded;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 velocity = Vector3.zero;

    private float x;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        grounded = true;
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal" + " " + gameObject.name) * (grounded ? groundMovementSpeed : airMovementSpeed);
       
        grounded = Physics2D.OverlapBox(transform.position, new Vector3(0.55f, 0.1f, 0), 0, groundLayer);
        anim.SetBool("IsJumping", !grounded);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public void Movement()
    {
        Vector3 targetVelocity = new Vector2((x * Time.fixedDeltaTime) * 10f, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothing);
        if (x < 0 && facingRight || x > 0 && !facingRight) facingRight = Flip();
        anim.SetFloat("Speed", Mathf.Abs(x));
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.collider);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(0.55f, 0.1f, 0));
    }

    public bool GetFacingRight() { return facingRight; }
}