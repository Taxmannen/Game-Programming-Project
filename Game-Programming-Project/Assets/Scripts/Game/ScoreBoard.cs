using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [Header("Timer")]
    public float timer;

    [Header("Timer")]
    public TextMeshProUGUI textField;

    [Header("Goal")]
    public Collider2D goalTrigger;

    private bool triggered;
    private bool won;

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
    }

    public void SetWinState(bool state)
    {
        won = state;
        if (state) triggered = true;
    }
}