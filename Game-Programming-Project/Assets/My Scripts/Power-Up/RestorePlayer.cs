public class RestorePlayer : Powerup
{
    private PlayerStats ps;

    private float effectTime = 5;

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