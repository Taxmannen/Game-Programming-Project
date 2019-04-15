using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [Header("Timer")]
    public float timer;

    [Header("Goal")]
    public Collider2D goalTrigger;

    [Header("Timer")]
    public TextMeshProUGUI timerText;

    [Header("Popup")]
    public GameObject popup;
    public TextMeshProUGUI popupText;

    private bool triggered;

    private void Update()
    {
        if (timer > 0 && !triggered)
        {
            timer -= Time.deltaTime;
            timerText.text = "" + (int)(timer + 1);
        }
        else
        {
            if (!triggered) SetWinState(false, null);
        }
    }

    public void SetWinState(bool state, string winner)
    {
        string text = state ? "Winner :" + " " + winner : "You Lost";

        popupText.text = text;
        popup.SetActive(true);
        goalTrigger.enabled = false;
        triggered = true;
        Time.timeScale = 0;
    }
}