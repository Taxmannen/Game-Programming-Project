using UnityEngine;

public class Goal : MonoBehaviour
{
    public ScoreBoard scoreBoard;

    private bool triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !triggered)
        {
            Debug.Log("WINNER :" + " " + other.gameObject.name);
            scoreBoard.SetWinState(true);
            triggered = true;
        }
    }
}
