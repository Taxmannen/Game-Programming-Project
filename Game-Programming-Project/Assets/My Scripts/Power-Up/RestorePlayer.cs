using UnityEngine;

public class RestorePlayer : Powerup
{
    [SerializeField] private float effectTime = 5;

    private PlayerStats ps;

    private void Start()
    {
        AudioManager.INSTANCE.Play("Restore", 0.4f, 0.75f);
        ps = player.GetComponent<PlayerStats>();
        ps.Restore(effectTime);
        Invoke("DestroyObject", effectTime);
    }

    private void DestroyObject()
    {
        UseItem();
    }
}