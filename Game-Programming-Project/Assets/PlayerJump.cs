using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump")]
    public float jumpPower;
    public float jumpTime = 1;

    [Header("Fall")]
    public float fallMultiplier;
    public float lowJumpMultiplier;

    private Rigidbody2D rb;
    private PlayerController pc;
    private bool doubleJump;
    private float inAirTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump" + " " + gameObject.name))
        {
            if (pc.grounded) StartCoroutine(Jump(false));
            else if (doubleJump) StartCoroutine(Jump(true));
        }

        if (!pc.grounded)
        {
            inAirTimer += Time.deltaTime;
        }

        if (!doubleJump && pc.grounded)
        {
            Debug.Log("Time in Air:" + " " + inAirTimer);
            inAirTimer = 0;
            doubleJump = true;
        }
    }

    private IEnumerator Jump(bool isDoubleJump)
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

        if (isDoubleJump) doubleJump = false;
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
}