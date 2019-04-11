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

    private Collider2D goalTrigger;

    private void Start()
    {
        goalTrigger = goal.GetComponent<Collider2D>();
    }

    void Update()
    {
        if (timer > 0)
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

        distanceBetweenPlayers = Vector2.Distance(player1.position, player2.position);
        distanceToGoalPlayer1  = Vector2.Distance(player1.position, goal.position);
        distanceToGoalPlayer2  = Vector2.Distance(player2.position, goal.position);
    }
}