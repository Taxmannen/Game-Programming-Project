using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public float timer;
    public TextMeshProUGUI textField;

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            textField.text = "" + (int)timer;
        }
    }
}
