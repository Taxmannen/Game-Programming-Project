using UnityEngine;

public class HighScoreMenuState : State<Menu>
{
    public GameObject menu;

    private static HighScoreMenuState instance;

    private HighScoreMenuState()
    {
        if (instance != null) return;
        else instance = this;
    }

    public static HighScoreMenuState Instance
    {
        get
        {
            if (instance == null) new HighScoreMenuState();
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