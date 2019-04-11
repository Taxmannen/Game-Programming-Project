using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float startDelay;
    public float endDelay;
    public Vector2 attackForce;

    private Animator anim;
    private Collider2D col;
    private PlayerController pc;
    private float attackTime;

    private void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
        pc = transform.parent.GetComponent<PlayerController>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Attack" + " " + transform.parent.name))
        {
            //if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Player Attack")
            //{
                anim.SetBool("IsAttacking", true);
                col.enabled = true;
                StartCoroutine(Attack());
            //}
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && Time.time - attackTime > 0.2f)
        {
            Rigidbody2D otherBody = other.GetComponent<Rigidbody2D>();
            if (pc.GetFacingRight()) otherBody.AddForce(attackForce, ForceMode2D.Impulse);
            else otherBody.AddForce(new Vector2(-attackForce.x, attackForce.y), ForceMode2D.Impulse);
            attackTime = Time.time;
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(startDelay);
        col.enabled = true;
        yield return new WaitForSeconds(endDelay);
        col.enabled = false;
        anim.SetBool("IsAttacking", false);
    }
}