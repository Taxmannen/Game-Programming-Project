using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HighscorePopup : HighscoreManager
{
    [SerializeField] private GameObject inputField;

    private EventSystem eventSystem;
    public float Time { get; set; }

    private void Start()
    {
        eventSystem = EventSystem.current;  
    }

    private void Update()
    {
        if (eventSystem.currentSelectedGameObject == null) eventSystem.SetSelectedGameObject(inputField);
    }

    public void AddScore(string name)
    {
        if (Input.GetButtonDown("Submit") && name.Length > 0 && Time > 0)
        {
            SaveNewHighscore(Utils.RemoveDecimals(Time, 3), name, Utils.RemoveSpaceFromString(SceneManager.GetActiveScene().name));
            SceneManager.LoadScene("Menu");
        }
    }
}
