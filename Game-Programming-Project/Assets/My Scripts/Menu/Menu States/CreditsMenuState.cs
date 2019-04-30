using UnityEngine;

public class CreditsMenuState : State<Menu>
{
    public GameObject menu;

    private static CreditsMenuState INSTANCE;

    private CreditsMenuState()
    {
        if (INSTANCE != null) return;
        else INSTANCE = this;
    }

    public static CreditsMenuState Instance
    {
        get
        {
            if (INSTANCE == null) new CreditsMenuState();
            return INSTANCE;
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