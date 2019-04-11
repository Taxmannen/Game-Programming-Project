public class PlayerStats : Character
{
    public PlayerStats()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    public void SetDistances(float toPlayer, float toGoal, float otherPlayerToGoal)
    {
        DistanceToGoal = toGoal;
        DistanceToOtherPlayer = toPlayer;
        OtherPlayersDistanceToGoal = otherPlayerToGoal;
    }

    public float DistanceToOtherPlayer { get; private set; }
    public float DistanceToGoal { get; private set; }
    public float OtherPlayersDistanceToGoal { get; private set; }
}