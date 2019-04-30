using UnityEngine;

public class RestorePlayer : Item
{
    private PlayerStats playerStats;
    private ParticleSystem ps;

    private float effectTime = 5;

    private void Start()
    {
        AudioManager.INSTANCE.Play("Restore", 0.4f, 0.75f);
        playerStats = player.GetComponent<PlayerStats>();
        //Fy skam på dig Daniel :P
        foreach(ParticleSystem particle in player.GetComponentsInChildren<ParticleSystem>())
        {
            if (particle.gameObject.name.Contains("Healing")) ps = particle;
        }
        ps.Play();
        playerStats.Restore(effectTime);
        Invoke("DestroyObject", effectTime);
    }

    private void DestroyObject()
    {
        ps.Stop();
        UseItem();
    }
}