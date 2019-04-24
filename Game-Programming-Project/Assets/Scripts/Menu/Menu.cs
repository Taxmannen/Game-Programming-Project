using UnityEngine;
using StateMachine;

public enum MenuState { StartMenu, LevelSelectMenu, HighScoreMenu, CreditsMenu}

public class Menu : MonoBehaviour
{
    private MenuState currentState = MenuState.StartMenu;

    private /*public*/ StateMachine<Menu> stateMachine; // { get; set; }

    private void Start()
    {
        stateMachine = new StateMachine<Menu>(this);
        stateMachine.ChangeState(StartMenuState.Instance);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void PlayGame()
    {
        ChangeMenuState(MenuState.LevelSelectMenu, LevelSelectMenuState.Instance);
    }

    public void HighScoreMenu()
    {
        ChangeMenuState(MenuState.HighScoreMenu, HighScoreMenuState.Instance);
    }

    public void CreditsMenu()
    {
        ChangeMenuState(MenuState.CreditsMenu, CreditsMenuState.Instance);
    }

    public void ExitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void StartMenu()
    {
        ChangeMenuState(MenuState.StartMenu, StartMenuState.Instance);
    }

    private void ChangeMenuState(MenuState newEnumState, State<Menu> newState)
    {
        if (currentState != newEnumState)
        {
            currentState = newEnumState;
            stateMachine.ChangeState(newState);
        }
    }
}