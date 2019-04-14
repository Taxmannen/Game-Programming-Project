using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [Header("Timer")]
    public float timer;

    [Header("Timer")]
    public TextMeshProUGUI textField;

    [Header("Goal")]
    public Transform goal;

    [Header("Player")]
    public Transform player1;
    public Transform player2;

    [Header("Distances")]
    public float distanceBetweenPlayers;
    public float distanceToGoalPlayer1;
    public float distanceToGoalPlayer2;

    private bool triggered;
    private bool won;

    private Collider2D goalTrigger;
    private PlayerStats player1Stats;
    private PlayerStats player2Stats;

    private void Start()
    {
        goalTrigger = goal.GetComponent<Collider2D>();
        player1Stats = player1.GetComponent<PlayerStats>();
        player2Stats = player2.GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (timer > 0 && !won)
        {
            timer -= Time.deltaTime;
            textField.text = "" + (int)timer;
        }
        else
        {
            if (!triggered)
            {
                Debug.Log("You Lost!");
                goalTrigger.enabled = false;
                triggered = true;
            }
        }
        SetDistances();
    }

    private void SetDistances()
    {
        distanceBetweenPlayers = Vector2.Distance(player1.position, player2.position);
        distanceToGoalPlayer1 = Vector2.Distance(player1.position, goal.position);
        distanceToGoalPlayer2 = Vector2.Distance(player2.position, goal.position);
        player1Stats.SetDistances(distanceBetweenPlayers, distanceToGoalPlayer1, distanceToGoalPlayer2);
        player2Stats.SetDistances(distanceBetweenPlayers, distanceToGoalPlayer2, distanceToGoalPlayer1);
    }

    public void SetWinState(bool state)
    {
        won = state;
        if (state) triggered = true;
    }
}