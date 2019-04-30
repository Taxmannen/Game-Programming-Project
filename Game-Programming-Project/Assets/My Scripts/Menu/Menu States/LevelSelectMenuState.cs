using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelSelectMenuState : State<Menu>
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject firstSelectedButton;

    private static LevelSelectMenuState INSTANCE;

    private EventSystem eventSystem;
    private GameObject lastSelectedButton;

    private LevelSelectMenuState()
    {
        if (INSTANCE != null) return;
        else INSTANCE = this;
    }

    public static LevelSelectMenuState Instance
    {
        get
        {
            if (INSTANCE == null) new LevelSelectMenuState();
            return INSTANCE;
        }
    }

    public override void EnterState(Menu type)
    {
        if (eventSystem == null) eventSystem = EventSystem.current;
        menu.SetActive(true);
        eventSystem.SetSelectedGameObject(firstSelectedButton);
    }

    public override void ExitState(Menu type)
    {
        menu.SetActive(false);
    }

    public override void UpdateState(Menu type)
    {
        if (eventSystem.currentSelectedGameObject == null) eventSystem.SetSelectedGameObject(lastSelectedButton);
        else lastSelectedButton = eventSystem.currentSelectedGameObject;

        if (Input.GetButtonDown("Cancel")) type.StartMenu();
    }

    public void StartLevel(string level)
    {
        AudioManager.INSTANCE.Play("Button Press");
        SceneManager.LoadScene(level);
    }
}