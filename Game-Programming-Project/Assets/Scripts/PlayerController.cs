using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed;
    public float smoothing;
    public float jumpPower;

    [Header("Layers")]
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 velocity = Vector3.zero;

    private bool facingRight;
    private bool grounded;
    private bool jump;
    private float x;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        facingRight = true;
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal" + " " + gameObject.name) * movementSpeed;
        if (Input.GetButtonDown("Jump" + " " + gameObject.name) && grounded) jump = true;

        grounded = Physics2D.OverlapBox(transform.position, new Vector3(0.8f, 0.1f, 0), 0, groundLayer);
        anim.SetBool("IsJumping", !grounded);
    }

    private void FixedUpdate()
    {
        Movement();
        Jump();
    }

    private void Jump()
    {
        if (jump)
        {
            anim.SetBool("IsJumping", true);
            rb.AddForce(new Vector2(0f, jumpPower));
            jump = false;
        }
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(0.8f, 0.1f, 0));
    }
}