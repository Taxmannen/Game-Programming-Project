using UnityEngine;

public class RestorePlayer : Item
{
    private PlayerStats playerStats;
    private ParticleSystem ps;

    private float effectTime = 5;

    private void Start()
    {
        playerStats = player.GetComponent<PlayerStats>();

        //Fy skam på dig Daniel :P
        foreach(ParticleSystem particle in player.GetComponentsInChildren<ParticleSystem>())
        {
            if (particle.gameObject.name.Contains("Healing")) ps = particle;
        }
        ps.Play();
        Invoke("DestroyObject", effectTime);
    }

    private void Update()
    {
        playerStats.Restore();
    }

    private void DestroyObject()
    {
        ps.Stop();
        UseItem();
    }
}