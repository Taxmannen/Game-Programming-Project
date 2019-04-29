using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HighscorePopup : HighscoreManager
{
    [SerializeField] private GameObject inputField;
    [SerializeField] private TextMeshProUGUI headerText;

    private EventSystem eventSystem;
    public float Time { get; set; }

    private void Start()
    {
        eventSystem = EventSystem.current;  
    }

    public void SetHeaderText(string winner)
    {
        headerText.text = "Winner:" + " " + winner;
    }

    private void Update()
    {
        if (eventSystem.currentSelectedGameObject == null) eventSystem.SetSelectedGameObject(inputField);
    }

    public void AddScore(string name)
    {
        if (Input.GetButtonDown("Submit") && name.Length > 0 && Time > 0)
        {
            SaveNewHighscore(MyUtils.RemoveDecimals(Time, 3), name, MyUtils.RemoveSpaceFromString(SceneManager.GetActiveScene().name));
            MyUtils.PauseGame(false);
            SceneManager.LoadScene("Menu");
        }
    }
}
