using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Restart")) SceneManager.LoadScene("Main");
    }
}
