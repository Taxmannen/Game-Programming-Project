using UnityEngine;
using StateMachine;

public enum MenuState { StartMenu, LevelSelectMenu, HighScoreMenu, CreditsMenu }

public class Menu : MonoBehaviour
{
    private MenuState currentState = MenuState.StartMenu;

    private /*public*/ StateMachine<Menu> stateMachine; // { get; set; }
    private bool applicationStart = true;

    private void Start()
    {
        stateMachine = new StateMachine<Menu>(this);
        stateMachine.ChangeState(StartMenuState.Instance);
        if (Application.isEditor) Cursor.visible = true;
        else                      Cursor.visible = false;
    }

    private void Update()
    {
        if (stateMachine != null) stateMachine.Update();
    }

    public void PlayGame()
    {
        PlaySound();
        ChangeMenuState(MenuState.LevelSelectMenu, LevelSelectMenuState.Instance);
    }

    public void HighScoreMenu()
    {
        PlaySound();
        ChangeMenuState(MenuState.HighScoreMenu, HighScoreMenuState.Instance);
    }

    public void CreditsMenu()
    {
        PlaySound();
        ChangeMenuState(MenuState.CreditsMenu, CreditsMenuState.Instance);
    }

    public void ExitGame()
    {
        PlaySound();
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void StartMenu()
    {
        PlaySound();
        ChangeMenuState(MenuState.StartMenu, StartMenuState.Instance);
    }

    public void PlaySound()
    {
        if (!applicationStart && !AudioManager.INSTANCE.IsPlaying("Button Press"))
            AudioManager.INSTANCE.Play("Button Press");
        else applicationStart = false;
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