using UnityEngine;
using UnityEngine.EventSystems;

public class StartMenuState : State<Menu>
{
    public GameObject menu;

    private static StartMenuState instance;

    private EventSystem eventSystem;
    private GameObject lastSelectedButton;

    private StartMenuState()
    {
        if (instance != null) return;
        else instance = this;
    }

    public static StartMenuState Instance
    {
        get
        {
            if (instance == null) new StartMenuState();
            return instance;
        }
    }

    public override void EnterState(Menu type)
    {
        if (eventSystem == null) eventSystem = EventSystem.current;
        menu.SetActive(true);
        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(lastSelectedButton);
    }

    public override void ExitState(Menu type)
    {
        menu.SetActive(false);
    }

    public override void UpdateState(Menu type)
    {
        if (eventSystem.currentSelectedGameObject == null) eventSystem.SetSelectedGameObject(lastSelectedButton);
        else lastSelectedButton = eventSystem.currentSelectedGameObject;
    }
}