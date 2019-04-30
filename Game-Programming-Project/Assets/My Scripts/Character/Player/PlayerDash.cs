using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    #region Variables
    [Header("General")]
    [SerializeField] private float cooldown = 0.2f;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 20;
    [SerializeField] private float dashTime = 0.4f;

    [Header("Slide")]
    [SerializeField] private float slideSpeed = 17.5f;
    [SerializeField] private float slideTime = 0.25f;

    private Animator anim;
    private Rigidbody2D rb;
    private PlayerController pc;
    private Coroutine coroutine;

    private float previousActionTime;
    private float lastDirection;
    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
        previousActionTime = Time.time - cooldown;

        if (pc.GetFacingRight()) lastDirection = 1;
        else                     lastDirection = -1;
    }

    private void Update()
    {
        if (!pc.UnableToMove && Input.GetButtonDown("Dash" + " " + gameObject.name))
        {
            if (coroutine == null && Time.time - previousActionTime > cooldown)
            {
                if (pc.grounded) coroutine = StartCoroutine(Dash("IsSliding", slideSpeed, slideTime));
                else             coroutine = StartCoroutine(Dash("IsDashing", dashSpeed, dashTime));
            }
        }

        float x = Input.GetAxis("Horizontal" + " " + gameObject.name);
        if (x != 0)
        {
            if (x < 0) lastDirection = -1;
            else       lastDirection = 1; 
        }
    }

    public void StopDash()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        anim.SetBool("IsSliding", false);
        anim.SetBool("IsDashing", false);
        previousActionTime = Time.time;
        coroutine = null;
    }

    private IEnumerator Dash(string animation, float speed, float actionTime)
    {
        float pitch = animation == "IsSliding" ? 0.8f : 1;
        AudioManager.INSTANCE.Play("Dash", 0.75f, pitch);
        anim.SetBool(animation, true);

        float timer = 0;
        while (timer < actionTime)
        {
            //float velocityX = (lastDirection * (speed * 50)) * Time.deltaTime;
            float velocityX = lastDirection * speed;
            rb.velocity =  new Vector2(velocityX, rb.velocity.y * 0.15f);

            timer += Time.deltaTime;

            if (Input.GetButtonUp("Dash" + " " + gameObject.name)) timer = actionTime;

            yield return null;
        }
        StopDash();
    }
}