using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private ScoreBoard scoreBoard;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        { 
            scoreBoard.SetWinState(true, other.gameObject.name);
            Destroy(this);
        }
    }
}