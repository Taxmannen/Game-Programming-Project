using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private LevelTimer levelTimer;
    [SerializeField] private GameObject winnerPopup;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioManager.Instance.Play("Victory");
            levelTimer.SetWinState();
            HighscorePopup highscorePopup = Instantiate(winnerPopup).GetComponent<HighscorePopup>();
            highscorePopup.SetHeaderText(other.gameObject.name);
            highscorePopup.Time = levelTimer.GetTime();
            Destroy(this);
        }
    }
}