using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [Header("Timer")]
    public float timer;

    [Header("Setup")]
    public TextMeshProUGUI textField;
    public Collider2D goalTrigger;

    private bool triggered;

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
    }
}