using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Popup")]
    [SerializeField] private GameObject popup;
    [SerializeField] private TextMeshProUGUI popupText;

    private float timer;
    private bool triggered;

    private void Update()
    {
        if (!triggered)
        {
            timer += Time.deltaTime;
            timerText.text = "" + (int)timer;
        }
    }

    public void SetWinState(bool state, string winner)
    {
        string text = state ? "Winner :" + " " + winner : "You Lost";

        popupText.text = text;
        popup.SetActive(true);
        triggered = true;
        Time.timeScale = 0;
    }
}