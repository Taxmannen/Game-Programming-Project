using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public float timer;
    public TextMeshProUGUI textField;

    void Update()
    {
        timer -= Time.deltaTime;
        textField.text = "" + (int)timer;

        if (Input.GetButtonDown("Restart")) SceneManager.LoadScene("Main");
    }
}
