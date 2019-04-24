using UnityEngine;

public class StartMenuState : State<Menu>
{
    public GameObject text;

    private static StartMenuState instance;

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
        Debug.Log("Entering Start Menu State");
        text.SetActive(true);
    }

    public override void ExitState(Menu type)
    {
        Debug.Log("Exiting Start Menu State");
        text.SetActive(false);
    }

    public override void UpdateState(Menu type)
    {
        //Debug.Log("Updating Start Menu State");
        return;
    }
}