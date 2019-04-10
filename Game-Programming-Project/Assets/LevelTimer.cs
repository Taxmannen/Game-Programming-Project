using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public float timer;
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
