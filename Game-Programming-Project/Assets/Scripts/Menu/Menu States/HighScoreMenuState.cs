using UnityEngine;

public class HighScoreMenuState : State<Menu>
{
    public GameObject text;

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
        Debug.Log("Entering High Score Menu State");
        if (text != null) text.SetActive(true);
    }

    public override void ExitState(Menu type)
    {
        Debug.Log("Exiting High Score Menu State");
        if (text != null) text.SetActive(false);
    }

    public override void UpdateState(Menu type)
    {
        //Debug.Log("Updating High Score Menu State");
        return;
    }
}