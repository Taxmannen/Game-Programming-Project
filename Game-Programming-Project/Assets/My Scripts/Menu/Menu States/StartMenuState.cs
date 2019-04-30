//using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartMenuState : State<Menu>
{
    public GameObject menu;

    private static StartMenuState INSTANCE;

    private EventSystem eventSystem;
    private GameObject lastSelectedButton;

    /*public Transform buttons;
    private int index;*/

    private StartMenuState()
    {
        if (INSTANCE != null) return;
        else INSTANCE = this;
    }

    public static StartMenuState Instance
    {
        get
        {
            if (INSTANCE == null) new StartMenuState();
            return INSTANCE;
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
        /*index = lastSelectedButton.transform.GetSiblingIndex();
        ChangeFontColor(buttons);*/
    }

    /*public void ChangeFontColor(Transform buttons)
    {
        foreach (Transform current in buttons)
        {
            if (current.GetSiblingIndex() == index) current.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            else current.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
    }*/
}