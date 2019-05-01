using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] private LevelTimer levelTimer;
    [SerializeField] private GameObject winnerPopup;

    private bool player1;
    private bool player2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (SceneManager.GetActiveScene().name != "Tutorial")
            {
                AudioManager.INSTANCE.Play("Victory");
                levelTimer.SetWinState();
                HighscorePopup highscorePopup = Instantiate(winnerPopup).GetComponent<HighscorePopup>();
                highscorePopup.SetHeaderText(other.gameObject.name);
                highscorePopup.Time = levelTimer.GetTime();
                Destroy(this);
            }
            else
            {
                if (other.gameObject.name == "Player 1") player1 = true;
                if (other.gameObject.name == "Player 2") player2 = true;

                if (player1 && player2) SceneManager.LoadScene("Menu");
            }
        }
    }
}