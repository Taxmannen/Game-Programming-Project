using UnityEngine;

public class CreditsMenuState : State<Menu>
{
    public GameObject menu;

    private static CreditsMenuState instance;

    private CreditsMenuState()
    {
        if (instance != null) return;
        else instance = this;
    }

    public static CreditsMenuState Instance
    {
        get
        {
            if (instance == null) new CreditsMenuState();
            return instance;
        }
    }

    public override void EnterState(Menu type)
    {
        menu.SetActive(true);
    }

    public override void ExitState(Menu type)
    {
        menu.SetActive(false);
    }

    public override void UpdateState(Menu type)
    {
        if (Input.GetButtonDown("Cancel")) type.StartMenu();
        return;
    }
}