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

    //private float attackDelay;

    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
        pc = GetComponent<PlayerController>();

        BoxCollider2D[] cols = GetComponents<BoxCollider2D>();
        foreach(BoxCollider2D c in cols)
        {
            if (c.isTrigger) col = c;
        }
    }

    private void Update()
    {
        if (!pc.UnableToMove && Input.GetButtonDown("Attack" + " " + transform.name))
        {
            if (coroutine == null && pc.grounded) coroutine = StartCoroutine(Attack());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !other.isTrigger /*&& Time.time - attackDelay > 0f*/)
        {
            PlayerController otherPc = other.GetComponent<PlayerController>();
            Vector2 force = pc.GetFacingRight() ? attackForce : new Vector2(-attackForce.x, attackForce.y);
            otherPc.HitPlayer(force, attackTime);
            //attackDelay = Time.time;
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