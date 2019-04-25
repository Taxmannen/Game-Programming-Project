using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Variables
    [Header("Attack")]
    [SerializeField] private float attackTime;
    [SerializeField] private Vector2 attackForce;

    [Header("Animation")]
    [SerializeField] private float startDelay;
    [SerializeField] private float endDelay;

    private Animator anim;
    private Collider2D col;
    private PlayerController pc;
    private Coroutine coroutine;

    private float attackDelay;
    #endregion

    private void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
        pc = transform.parent.GetComponent<PlayerController>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!pc.UnableToMove && Input.GetButtonDown("Attack" + " " + transform.parent.name))
        {
            if (coroutine == null && pc.grounded) coroutine = StartCoroutine(Attack());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && Time.time - attackDelay > 0.2f)
        {
            PlayerController otherPc = other.GetComponent<PlayerController>();
            Vector2 force = pc.GetFacingRight() ? attackForce : new Vector2(-attackForce.x, attackForce.y);
            otherPc.HitPlayer(force, attackTime);
            attackDelay = Time.time;
        }
    }

    private IEnumerator Attack()
    {
        anim.SetBool("IsAttacking", true);

        yield return new WaitForSeconds(startDelay);
        col.enabled = true;
        anim.SetBool("IsAttacking", false);

        yield return new WaitForSeconds(endDelay);
        col.enabled = false;
        coroutine = null;
    }
}