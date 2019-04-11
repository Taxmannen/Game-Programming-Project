using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Players { singleplayer, multiplayer }

    [Header("State")]
    public Players players;
    [Header("Debug")]
    public bool runThis;

    [Header("Setup")]
    public GameObject player1;
    public GameObject player2;
    public GameObject singleplayerCamera;
    public GameObject multiplayerCamera;

    void Start()
    {
        if (runThis)
        {
            if (players == Players.singleplayer)
            {
                player1.transform.position = new Vector3(0, -3.05f, 0);
                player1.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
                player2.SetActive(false);
                singleplayerCamera.SetActive(true);
                multiplayerCamera.SetActive(false);
            }
            else
            {
                player1.transform.position = new Vector3(-4, -3.05f, 0);
                player2.transform.position = new Vector3(4, -3.05f, 0);
                player1.GetComponent<SpriteRenderer>().color = new Color(255, 165, 165);
                player2.GetComponent<SpriteRenderer>().color = new Color(165, 255, 165);
                singleplayerCamera.SetActive(false);
                multiplayerCamera.SetActive(true);
            }
        }
    }
}
