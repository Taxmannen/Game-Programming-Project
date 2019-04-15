using UnityEngine;

public class Goal : MonoBehaviour
{
    public ScoreBoard scoreBoard;

    private bool triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !triggered)
        {
            scoreBoard.SetWinState(true, other.gameObject.name);
            triggered = true;
        }
    }
}