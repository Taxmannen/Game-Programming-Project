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

    private Collider2D col;
    private PlayerController pc;
    private Coroutine coroutine;
    #endregion

    private void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !other.isTrigger)
        {
            PlayerController otherPc = other.GetComponent<PlayerController>();
            Vector2 force = pc.GetFacingRight() ? attackForce : new Vector2(-attackForce.x, attackForce.y);
            otherPc.HitPlayer(force, attackTime);
        }
    }
}