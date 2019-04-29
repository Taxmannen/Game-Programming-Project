using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private ScoreBoard scoreBoard;
    [SerializeField] private GameObject winnerPopup;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioManager.Instance.Play("Victory");
            scoreBoard.SetWinState();
            HighscorePopup highscorePopup = Instantiate(winnerPopup).GetComponent<HighscorePopup>();
            highscorePopup.SetHeaderText(other.gameObject.name);
            highscorePopup.Time = scoreBoard.GetTime();
            Destroy(this);
        }
    }
}