using UnityEngine;
using StateMachine;

public enum MenuState { StartMenu, HighScoreMenu }

public class Menu : MonoBehaviour
{
    private MenuState currentState = MenuState.StartMenu;

    private StateMachine<Menu> stateMachine;

    private void Start()
    {
        stateMachine = new StateMachine<Menu>(this);
        stateMachine.ChangeState(StartMenuState.Instance);
    }

    private void Update()
    {
        if (currentState != MenuState.StartMenu && Input.GetKeyDown(KeyCode.A))
        {
            currentState = MenuState.StartMenu;
            stateMachine.ChangeState(StartMenuState.Instance);
        }
        else if (currentState != MenuState.HighScoreMenu && Input.GetKeyDown(KeyCode.D))
        {
            currentState = MenuState.HighScoreMenu;
            stateMachine.ChangeState(HighScoreMenuState.Instance);
        }

        stateMachine.Update();
    }
}