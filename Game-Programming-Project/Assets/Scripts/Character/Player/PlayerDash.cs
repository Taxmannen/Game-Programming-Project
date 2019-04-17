using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [Header("Dash and Slide")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;

    //[Header("Debug")]

    private Animator anim;
    private Rigidbody2D rb;
    private PlayerController pc;
    private Coroutine coroutine;

    private float lastDash;
    private float lastDirection;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
        lastDash = Time.time - dashCooldown;

        if (pc.GetFacingRight()) lastDirection = 1;
        else                     lastDirection = -1;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Slide" + " " + gameObject.name))
        {
            if (coroutine == null && Time.time - lastDash > dashCooldown)
            {
                if (pc.grounded) coroutine = StartCoroutine(Slide());
                else coroutine = StartCoroutine(Dash());
            }
        }

        float x = Input.GetAxis("Horizontal" + " " + gameObject.name);
        if (x != 0)
        {
            if (x < 0) lastDirection = -1;
            else       lastDirection = 1; 
        }
    }

    private IEnumerator Slide()
    {
        anim.SetBool("IsSliding", true);

        float timer = 0;
        while (timer < dashTime)
        {

            float velocityX = (lastDirection * (dashSpeed * 50)) * Time.deltaTime;
            rb.velocity =  new Vector2(velocityX, rb.velocity.y);

            timer += Time.deltaTime;

            if (Input.GetButtonUp("Slide" + " " + gameObject.name)) timer = dashTime;

            yield return null;
        }
        anim.SetBool("IsSliding", false);

        lastDash = Time.time;
        coroutine = null;
    }

    private IEnumerator Dash()
    {
        anim.SetBool("IsDashing", true);

        float timer = 0;
        while (timer < dashTime)
        {
            yield return null;
        }
        anim.SetBool("IsDashing", false);

        lastDash = Time.time;
        coroutine = null;
    }
}
