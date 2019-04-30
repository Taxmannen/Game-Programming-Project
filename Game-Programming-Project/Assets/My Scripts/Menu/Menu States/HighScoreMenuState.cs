using UnityEngine;

public class HighScoreMenuState : State<Menu>
{
    public GameObject menu;

    private static HighScoreMenuState INSTANCE;

    private HighScoreMenuState()
    {
        if (INSTANCE != null) return;
        else INSTANCE = this;
    }

    public static HighScoreMenuState Instance
    {
        get
        {
            if (INSTANCE == null) new HighScoreMenuState();
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